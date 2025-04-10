using nps.Models.DTOS;

namespace nps.Services.Survey;

using Models.SurveyQuestions;

public interface ISurveyService
{
     Task<Survey?> GetSurveyAsync(long surveyId);
    
     Task<Survey?> GetSurveyByOrderNumber(string orderNumber, bool readOnly = false);
     
     [Transactional]
     Task<int> SaveSurveyResponses(SurveyResponseDto responses);
     
     [Transactional]
     Task CreateSurveyForOrder(SurveyCreationDto surveyCreatorData, string orderNumber);

     Task<List<SurveyView>> GetAll();
     
     Task AssignSurveyForOrder(long surveyId, string orderNumber);
}