using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nps.Models;
using nps.Models.SurveyQuestions;

namespace nps;

public static class UtilityExtensions
{
    public static void SetQuestionTypes(this DiscriminatorBuilder<int> discriminator)
    {
        discriminator
            .HasValue<RatingQuestion>((int)AnswerType.Rating)
            .HasValue<TextQuestion>((int)AnswerType.Text)
            .HasValue<SingleChoiceQuestion>((int)AnswerType.SingleChoice)
            .HasValue<DropDownQuestion>((int)AnswerType.DropDownList);
    }
}