using nps.Models.SurveyQuestions;

namespace nps.Models.DTOS;

public record QuestionSurveyPair<TQuestion>(TQuestion Question, SurveyDto Dto)
    where TQuestion : Question;