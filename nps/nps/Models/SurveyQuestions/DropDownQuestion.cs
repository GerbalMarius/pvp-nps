using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;

public class DropDownQuestion : Question
{
    [Column("options")]
    public List<string> Options { get; set; } = [];
}