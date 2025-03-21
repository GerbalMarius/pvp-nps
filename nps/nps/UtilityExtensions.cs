using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nps.Models.SurveyQuestions;

namespace nps;

public static class UtilityExtensions
{
    /// <summary>
    /// Used as a helper method to generate different input types , based on the typeof question passed.
    /// Mainly useful for bulk operations on survey questions.
    /// It is also useful, because im lazy and i cba writing the whole logic on survey display page ;).
    /// </summary>
    /// <param name="helper">HtmlHelper class to invoke this method from.</param>
    /// <param name="question">The question to process.</param>
    /// <param name="checkBoxBufferName">The list property name which stores the checkbox question options,</param>
    /// <typeparam name="TModel">The type of model to use this helper method on.</typeparam>
    /// <returns></returns>
    public static IHtmlContent RenderQuestion<TModel>(
        this IHtmlHelper<TModel> helper,
        Question question,
        string? checkBoxBufferName = null)
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
                HandleTextQuestion(textQuestion, sb);
                break;
            case CheckBoxQuestion checkBoxQuestion:
                HandleCheckBoxQuestion(checkBoxQuestion, sb, checkBoxBufferName);
                break;
            
            case SingleChoiceQuestion singleChoiceQuestion:
                HandleSingleChoiceQuestion(singleChoiceQuestion, sb);
                break;
                
        }
        return new HtmlString(sb.ToString());
    }

    private static void HandleSingleChoiceQuestion(SingleChoiceQuestion singleChoiceQuestion, StringBuilder sb)
    {
        sb.AppendLine("<div>");
        sb.AppendLine($"<p class='quiz-question'>{singleChoiceQuestion.QuestionText}</p>");
        sb.AppendLine("<div class='quiz-options'>");
        foreach (var choice in singleChoiceQuestion.Choices)
        {
            sb.AppendLine("<label class='quiz-option'>");
            sb.AppendLine($"<input type='radio' name='single{singleChoiceQuestion.Id}' value='{choice.Text}'>{choice.Text}");
            sb.AppendLine("</label>");
        }
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");
    }

    private static void HandleCheckBoxQuestion(CheckBoxQuestion checkBoxQuestion, StringBuilder sb, string? checkBoxBufferName)
    {
        sb.AppendLine("<div>");
        sb.AppendLine("<p></p>");
        sb.AppendLine("</div>");
    }

    private static void HandleTextQuestion(TextQuestion textQuestion, StringBuilder sb)
    {
        
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
        sb.AppendLine($"<select class='quiz-dropdown' name='dropdown_{dropDownQuestion.Id}'>");
        sb.AppendLine("<option value='' disabled selected>Select an option...</option>");
        foreach (var choice in dropDownQuestion.Choices)
        {
            sb.AppendLine($"<option value='{string.Join('_',choice.Text.ToLower().Split("\\s"))}'>{choice.Text}</option");//my brain is fried ;(
        }
        sb.AppendLine("</select>");
        sb.AppendLine("</div>");
    }
    
    
}