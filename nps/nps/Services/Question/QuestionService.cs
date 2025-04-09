using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;

namespace nps.Services.Question;

public sealed class QuestionService : IQuestionService
{
    private readonly AppDbContext _db;
    private readonly ILogger<QuestionService> _logger;

    public QuestionService(AppDbContext db, ILogger<QuestionService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Models.SurveyQuestions.Question>> GetAll()
    {
        return await _db.Questions.AsNoTracking()
                                  .ToListAsync();
    }

    public async Task<Models.SurveyQuestions.Question> GetById(long questionId)
    {
        return await _db.Questions.SingleAsync(question => question.Id == questionId);
    }
}