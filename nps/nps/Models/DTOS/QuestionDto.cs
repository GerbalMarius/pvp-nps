namespace nps.Models.DTOS;

public record QuestionDto(
    string QuestionText,
    List<string> CheckBoxChoices,
    int MaxOptions
        );