using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

public class DropDownQuestion : Question
{
    public ICollection<AnswerChoice> Choices { get; set; }
}