using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;
using nps.Models;
using nps.Models.DTOS;
using nps.Models.SurveyQuestions;

namespace nps.Services.Survey;

public sealed class SurveyService : ISurveyService
{
    private readonly ILogger<SurveyService> _logger;
    
    private readonly AppDbContext _dbContext;

    public SurveyService(ILogger<SurveyService> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<Models.SurveyQuestions.Survey?> GetSurveyAsync(long surveyId)
    {
        return await _dbContext.Surveys.FindAsync(surveyId);
    }

    public async Task<Models.SurveyQuestions.Survey?> GetSurveyByOrderNumber(string orderNumber, bool readOnly = false)
    {
        var result =   _dbContext.Surveys
            .Include(s => s.Orders)
            .Include(s => s.Questions)
            .ThenInclude(q => q.Choices)
            .Where(s => s.Orders.Any(o => o.Number == orderNumber && o.SurveyId.HasValue))
            .AsSplitQuery();

        return readOnly ? await result.AsNoTracking().SingleOrDefaultAsync() : await result.SingleOrDefaultAsync();
    }
    
    public Task<List<SurveyView>> GetAll()
    {
        return _dbContext.Surveys.Select(survey => new SurveyView(survey.Id, survey.Name, survey.CreatedAt))
                                 .ToListAsync();
    }

    public async Task AssignSurveyForOrder(long surveyId, string orderNumber)
    {
        var survey = await _dbContext.Surveys.FindAsync(surveyId);
        if (survey is null)
        {
            throw new Exception($"Survey {surveyId} not found in database.");
        }
        
        var order = await _dbContext.Orders.SingleAsync(order => order.Number == orderNumber);
        
        survey.Orders.Add(order);
        order.SurveyId =  survey.Id;
       await _dbContext.SaveChangesAsync();
    }


    [Transactional]
    public async Task CreateSurveyForOrder(SurveyCreationDto surveyCreatorData, string orderNumber)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var order = await _dbContext.Orders.SingleAsync(o => o.Number == orderNumber);

            var survey = new Models.SurveyQuestions.Survey
            {
                CreatedAt = DateTime.Now,
                Name = surveyCreatorData.Name
            };

            foreach (var questionData in surveyCreatorData.Questions)
            {
                Models.SurveyQuestions.Question question;
                if (questionData is { ExistingQuestionId: not null, UseExisting: true })
                {
                    question = (await _dbContext.Questions.FindAsync(questionData.ExistingQuestionId.Value))!;
                }
                else
                {
                   question =  DetermineQuestionType(questionData);
                   _dbContext.Questions.Add(question);
                }
                survey.Questions.Add(question);
                
            }
            _dbContext.Surveys.Add(survey);
            
            survey.Orders.Add(order);
            order.SurveyId = survey.Id;
            
            await _dbContext.SaveChangesAsync();
            
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw new Exception("Unable to assign survey to an order.");
        }
        
    }

    private static Models.SurveyQuestions.Question DetermineQuestionType(QuestionDto q)
    {
        return q.Type switch
        {
            QuestionType.SingleChoice => new SingleChoiceQuestion
            {
                QuestionText = q.QuestionText!,
                MaxOptions = q.MaxOptions ?? 2
            },
            QuestionType.Rating => new RatingQuestion
            {
                QuestionText = q.QuestionText!
            },
            QuestionType.DropDown => new DropDownQuestion
            {
                QuestionText = q.QuestionText!,
                Choices = Utils.ExtractChoices(q).Select(ch => new AnswerChoice
                {
                    Text = ch
                }).ToList()
            },
            QuestionType.Checkbox => new CheckBoxQuestion
            {
                QuestionText = q.QuestionText!,
                Choices = Utils.ExtractChoices(q).Select(ch => new AnswerChoice
                {
                    Text = ch
                }).ToList()
            },
            QuestionType.Text => new TextQuestion
            {
                QuestionText = q.QuestionText!
            },
            _ => throw new Exception("Unable to determine question type.")
        };
    }

    [Transactional]
    public async Task<int> SaveSurveyResponses(SurveyResponseDto responses)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        List<Response> responsesToSave = [];  
        
        List<ResponseOption> optionsToSave = []; 
        
        DateTime saveDate = DateTime.Now;
        try
        {
             SaveCheckBoxResponses(responses, responsesToSave, optionsToSave, saveDate); 
            
             SaveDropDownResponses(responses, responsesToSave, saveDate);
            
             SaveRatingResponses(responses, responsesToSave, saveDate);
            
             SaveSingleChoiceResponses(responses, responsesToSave, saveDate);
            
             _dbContext.Responses.AddRange(responsesToSave);
             _dbContext.ResponseOptions.AddRange(optionsToSave);
            
            int rowsAffected = await _dbContext.SaveChangesAsync();
        
            await transaction.CommitAsync();
            return rowsAffected;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Unable to save the survey responses.", ex);
        }
    }

    private static void SaveCheckBoxResponses(SurveyResponseDto responses, 
        List<Response> responsesToSave,
        List<ResponseOption> optionsToSave, 
        DateTime timeStamp)
    {
        foreach (var (questionId, checkBoxList) in responses.CheckBoxAnswers)
        {
            var response = new Response
            {
                QuestionId = questionId,
                ResponseDate = timeStamp,
            };
                
            responsesToSave.Add(response);
                
            optionsToSave.AddRange([..checkBoxList.Select(option => new ResponseOption 
            {
                Response = response,
                OptionText = option
            })]);
        }
    }

    private static void SaveDropDownResponses(SurveyResponseDto responses, 
        List<Response> responsesToSave, 
        DateTime timeStamp)
    {
        foreach (var (questionId, dropDownOpt) in responses.DropDownAnswers)
        {
            var response = new Response
            {
                QuestionId = questionId,
                ResponseDate = timeStamp,
                TextAnswer = dropDownOpt
            };
            responsesToSave.Add(response);
            
        }

    }

    private static void SaveRatingResponses(SurveyResponseDto responses, 
        List<Response> responsesToSave, 
        DateTime timeStamp)
    {
        foreach (var (questionId, npsRating) in responses.RatingAnswers)
        {
            var response = new Response
            {
                QuestionId = questionId,
                ResponseDate = timeStamp,
                NumberRating = npsRating
            };
            responsesToSave.Add(response);
        }
    }

    private static void SaveSingleChoiceResponses(SurveyResponseDto responses, List<Response> responsesToSave, DateTime timeStamp)
    {
        foreach (var (questionId, choice) in responses.SingleChoiceAnswers)
        {
            var response = new Response
            {
                QuestionId = questionId,
                ResponseDate = timeStamp,
                TextAnswer = choice
            };
            responsesToSave.Add(response);
        }
    }
}