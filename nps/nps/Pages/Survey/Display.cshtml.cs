using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Services.Survey;

namespace nps.Pages.Survey;

public class Display : PageModel
{
    
    private readonly ILogger<Display> _logger;
    private readonly ISurveyService _surveyService;
    
    public Models.SurveyQuestions.Survey? SurveyToDisplay { get; set; }
    
    [BindProperty] 
    public List<string> Buffer { get; } = [];

    public Display(ILogger<Display> logger, ISurveyService surveyService)
    {
        _logger = logger;
        _surveyService = surveyService;
    }

    public async Task<IActionResult> OnGetAsync(string orderNumber)
    {
        SurveyToDisplay = await _surveyService.FetchSurveyByOrderNumber(orderNumber);

        if (SurveyToDisplay == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string orderNumber)
    {
        
        SurveyToDisplay = await _surveyService.FetchSurveyByOrderNumber(orderNumber);

        if (SurveyToDisplay == null)
        {
            return NotFound();
        }

        return RedirectToPage("Display", new {orderNumber, success = true });
    }
}