using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using nps.Migrations.Data;
using nps.Models;
using nps.Models.DTOS;

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
            .Where(s => s.Orders.Any(o => o.Number == orderNumber && o.HasSurvey))
            .AsSplitQuery();

        return readOnly ? await result.AsNoTracking().SingleOrDefaultAsync() : await result.SingleOrDefaultAsync();
    }

    public async Task<int> SaveSurveyResponses(SurveyDto responses)
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

    private static void SaveCheckBoxResponses(SurveyDto responses, 
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

    private static void SaveDropDownResponses(SurveyDto responses, 
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

    private static void SaveRatingResponses(SurveyDto responses, 
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

    private static void SaveSingleChoiceResponses(SurveyDto responses, List<Response> responsesToSave, DateTime timeStamp)
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