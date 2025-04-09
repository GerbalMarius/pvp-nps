using nps.Models.DTOS;

namespace nps.Services.Survey;

using Models.SurveyQuestions;

public interface ISurveyService
{
     Task<Survey?> GetSurveyAsync(long surveyId);
    
     Task<Survey?> GetSurveyByOrderNumber(string orderNumber, bool readOnly = false);
     
     Task<int> SaveSurveyResponses(SurveyDto responses);

}