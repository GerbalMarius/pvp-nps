using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using nps.Models;
using nps.Models.SurveyQuestions;

namespace nps;

public static class ModelBuilderExtensions
{
    public static void SetQuestionTypes(this DiscriminatorBuilder<int> discriminator)
    {
        discriminator
            .HasValue<RatingQuestion>((int)AnswerType.Rating)
            .HasValue<TextQuestion>((int)AnswerType.Text)
            .HasValue<YesNoQuestion>((int)AnswerType.YesNo)
            .HasValue<DropDownQuestion>((int)AnswerType.DropDownList);
    }
}