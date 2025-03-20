using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            .HasValue<DropDownQuestion>((int)AnswerType.DropDownList)
            .HasValue<CheckBoxQuestion>((int)AnswerType.CheckBox);
    }

    public static IHtmlContent RenderQuestion<TModel>(
        this IHtmlHelper<TModel> helper,
        Question question
    )
    {
        var sb = new StringBuilder();
        

        switch (question)
        {
            case RatingQuestion ratingQuestion:
                HandleRatingQuestion(ratingQuestion, sb);
                break;
            case DropDownQuestion dropDownQuestion:
                HandleDropDownQuestion(dropDownQuestion, sb);
                break;
            case TextQuestion textQuestion:
                break;
                
        }
        return new HtmlString(sb.ToString());
    }

    private static void HandleRatingQuestion(RatingQuestion ratingQuestion, StringBuilder sb)
    {
        sb.AppendLine("<div>");
        sb.AppendLine($"<p class='quiz-question'>{ratingQuestion.QuestionText}</p>");
        sb.AppendLine("<div class='quiz-rating-buttons'>");
        for (int i = 0; i < ratingQuestion.MaxValue; i++)
        {
            sb.AppendLine($"<button type='button' class='rating-btn' data-value='{i + 1}' onclick='selectNPS({i + 1}, 'selectedNps{ratingQuestion.Id}')'>{i + 1}</button>");
        }

        sb.AppendLine($"<input type='hidden' id='selectedNps{ratingQuestion.Id}' name='nps_{ratingQuestion.Id}'>");
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");
    }

    private static void HandleDropDownQuestion(DropDownQuestion dropDownQuestion, StringBuilder sb)
    {
        sb.AppendLine("<div>");
        sb.AppendLine($"<p class='quiz-question'>{dropDownQuestion.QuestionText}</p>");
        sb.AppendLine("<div class='quiz-options'>");
        foreach (var choice in dropDownQuestion.Choices)
        {
            sb.AppendLine($"<label class='quiz-option' for='{choice.Id}'>");
            sb.AppendLine("");
        }
    }
    
}