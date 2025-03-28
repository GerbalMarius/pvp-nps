﻿using System.ComponentModel.DataAnnotations.Schema;

namespace nps.Models.SurveyQuestions;


[Table("single_choice_questions")]
public class SingleChoiceQuestion : Question
{
    [Column("max_options")]
    public int MaxOptions { get; set; } = 2;
}