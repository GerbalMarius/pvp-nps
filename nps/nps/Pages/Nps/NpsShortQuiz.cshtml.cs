using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models.SurveyQuestions;

namespace nps.Pages.Nps;

public class NpsShortQuiz : PageModel
{
    [BindProperty]
    public ShortNpsDto Result { get;  set; }
    
    private readonly ILogger<NpsShortQuiz> _logger;

    public NpsShortQuiz(ILogger<NpsShortQuiz> logger)
    {
        _logger = logger;
    }
    
    public IActionResult OnGet()
    {
        Result = new ShortNpsDto();
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        _logger.LogInformation($"{Result}");
        
        return Redirect("/Nps/NpsShortQuiz?success=true");
    }
}