using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using nps.Migrations.Data;
using nps.Models.SurveyQuestions;

namespace nps.Pages.Survey;

public class Display : PageModel
{
    public int OrderId { get; set; }

    public Models.SurveyQuestions.Survey SurveyToDisplay { get; set; }
    
    
    private readonly ILogger<Display> _logger;

    private readonly AppDbContext _db;

    public Display(AppDbContext db, ILogger<Display> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task<IActionResult> OnGet(int orderId)
    {
        OrderId = orderId;
        SurveyToDisplay = await _db.Surveys.Include(s => s.Questions)
            .ThenInclude(q => q.Choices)
            .AsNoTracking()
            .FirstAsync(s => s.OrderId == orderId);
        
        return Page();
    }
}