using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;

namespace nps.Pages.Survey;

public class Display : PageModel
{
    private readonly AppDbContext _db;


    private readonly ILogger<Display> _logger;
    
    public long OrderId { get; set; }

    public Models.SurveyQuestions.Survey? SurveyToDisplay { get; set; }
    
    [BindProperty] 
    public List<string> Buffer { get; } = [];

    public Display(AppDbContext db, ILogger<Display> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IActionResult> OnGet(long orderId)
    {
        OrderId = orderId;
        SurveyToDisplay = await _db.Surveys.Include(s => s.Questions)
            .ThenInclude(q => q.Choices)
            .Where(s => !s.TakenAt.HasValue)
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.OrderId == orderId);

        if (SurveyToDisplay == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPost(long orderId)
    {
        OrderId = orderId;
    
      
        SurveyToDisplay = await _db.Surveys.Include(s => s.Questions)
            .ThenInclude(q => q.Choices)
            .Where(s => !s.TakenAt.HasValue)
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.OrderId == orderId);

        if (SurveyToDisplay == null)
        {
            return NotFound();
        }

        return RedirectToPage("Display", new {orderId, success = true });
    }
}