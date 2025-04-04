namespace nps.Services.Survey;

using Models.SurveyQuestions;

public interface ISurveyService
{
     Task<Survey?> GetSurveyAsync(long surveyId);
    
    Task<Survey?> FetchSurveyByOrderId(long orderId);
}