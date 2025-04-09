using Microsoft.EntityFrameworkCore;
using nps.Models;

namespace nps;

public static class Utils
{
    /// <summary>
    /// Calculates nps score based on the formula = %promoters - %detractors,
    /// if nps score needs to be extracted within a given timeframe, filter the corresponding surveys by Datetime property 'TakenAt'.
    /// </summary>
    /// <param name="responses">The table of corresponding question responses.</param>
    /// <returns>The calculated nps score wrapped in a non-blocking task.</returns>
    public static async Task<double> CalcNpsScore(IQueryable<Response> responses)
    {
        var groupedResponses = await responses
            .Where(response => response.NumberRating.HasValue)
            .GroupBy(response => response.NumberRating >= 9 ? "Promoters" : response.NumberRating <= 6 ? "Detractors" : "Passives")
            .Select(group => new
            {
                Category = group.Key, 
                Count = group.Count()
            }) //Selects are non-tracking by default
            .ToListAsync();//to allow non-consuming behavior

        
        int totalResponses = groupedResponses.Sum(group => group.Count);
        if (totalResponses == 0) return 0;

        
        int promoters = groupedResponses.FirstOrDefault(group => group.Category == "Promoters")?.Count ?? 0;
        int detractors = groupedResponses.FirstOrDefault(group => group.Category == "Detractors")?.Count ?? 0;

        // Calculate percentages & NPS score
        double promoterPercentage = (double)promoters / totalResponses * 100;
        double detractorPercentage = (double)detractors / totalResponses * 100;

        return Math.Round(promoterPercentage - detractorPercentage);
    }

    public static IEnumerable<TResult> SelectIfPossible<T, TResult>(this IEnumerable<T> source, Predicate<T> predicate,
        Func<T, TResult> selector)
    {
        return from item in source where predicate(item) select selector(item);
    }
}