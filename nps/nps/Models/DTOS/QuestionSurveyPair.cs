using nps.Models.SurveyQuestions;

namespace nps.Models.DTOS;

public record QuestionSurveyPair<TQuestion>(TQuestion Question, SurveyResponseDto ResponseDto)
    where TQuestion : Question;