using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

[Table("dropdown_questions")]
public class DropDownQuestion : Question
{
}