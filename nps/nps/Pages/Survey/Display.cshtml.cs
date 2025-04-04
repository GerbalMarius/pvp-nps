using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;
using nps.Services.Survey;

namespace nps.Pages.Survey;

public class Display : PageModel
{
    private readonly AppDbContext _db;


    private readonly ILogger<Display> _logger;
    
    public long OrderId { get; set; }
    
    private readonly ISurveyService _surveyService;

    public Models.SurveyQuestions.Survey? SurveyToDisplay { get; set; }
    
    [BindProperty] 
    public List<string> Buffer { get; } = [];

    public Display(AppDbContext db, ILogger<Display> logger, ISurveyService surveyService)
    {
        _db = db;
        _logger = logger;
        _surveyService = surveyService;
    }

    public async Task<IActionResult> OnGetAsync(long orderId)
    {
        OrderId = orderId;
        SurveyToDisplay = await _surveyService.FetchSurveyByOrderId(orderId);

        if (SurveyToDisplay == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(long orderId)
    {
        OrderId = orderId;
    
      
        SurveyToDisplay = await _surveyService.FetchSurveyByOrderId(orderId);

        if (SurveyToDisplay == null)
        {
            return NotFound();
        }

        return RedirectToPage("Display", new {orderId, success = true });
    }
}