using System.ComponentModel.DataAnnotations;

namespace nps.Models.SurveyQuestions;

public record ShortNpsDto
{
    [Required(ErrorMessage = "Please select your mood.")]
    public int? Value { get; init; }
    
    public string? Body { get; init; }
};