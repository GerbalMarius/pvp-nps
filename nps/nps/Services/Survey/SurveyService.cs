using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;

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

    public async Task<Models.SurveyQuestions.Survey?> FetchSurveyByOrderNumber(string orderNumber)
    {
      return await _dbContext.Surveys
            .Include(s => s.Orders) 
            .Include(s => s.Questions)
            .ThenInclude(q => q.Choices)
            .Where(s => !s.TakenAt.HasValue)
            .Where(s => s.Orders.Any(o => o.Number == orderNumber))
            .AsSplitQuery()
            .FirstOrDefaultAsync();
    }
}