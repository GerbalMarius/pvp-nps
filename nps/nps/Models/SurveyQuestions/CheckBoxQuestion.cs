using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

public class CheckBoxQuestion : Question
{
    public ICollection<AnswerChoice> Choices { get; set; }
}