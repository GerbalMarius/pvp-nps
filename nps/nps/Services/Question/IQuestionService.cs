namespace nps.Services.Question;

public interface IQuestionService
{
    Task<List<Models.SurveyQuestions.Question>> GetAll();
    
    Task<Models.SurveyQuestions.Question> GetById(long questionId);
}