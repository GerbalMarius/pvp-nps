using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models.DTOS;

namespace nps.Pages.Auth;

public class Index : PageModel
{
    [BindProperty]
    public LoginInfo LoginData { get; set; } = new();
    
    [BindProperty]
    public RegisterInfo RegisterData { get; set; } = new();
    
    private readonly ILogger<Index> _logger;

    public Index(ILogger<Index> logger)
    {
        _logger = logger;
    }
    
    public IActionResult OnGet()
    {
        if (TempData["ModelStateErrors"] == null) return Page();
        
        PopulateErrors();

        return Page();
    }

    public IActionResult OnPost()
    {
        var formType = Request.Form["FormType"];

        if (formType == "Login")
        {
            ClearModelStateFor(key => !key.Contains("LoginData"));

            if (ModelState.IsValid) return Page();
            
            TempData["ModelStateErrors"] = KeepErrors(ModelState);
            return RedirectToPage("/Auth/Index", new { errorType = "Login" });

        }

        ClearModelStateFor(key => key.Contains("LoginData"));

        if (ModelState.IsValid) return Page();
            
        TempData["ModelStateErrors"] = KeepErrors(ModelState);
                
        return RedirectToPage("/Auth/Index", new { errorType = "Register" });
    }

    private void ClearModelStateFor(Func<string, bool> predicate)
    {
        foreach (var key in ModelState.Keys.Where(predicate))
        {
            ModelState.Remove(key);
        }
    }

    private string KeepErrors(ModelStateDictionary dictionary)
    {
        var errors = dictionary.ToDictionary(
            entry => entry.Key,
            entry => entry.Value.Errors.Select(e => e.ErrorMessage).ToArray()
        );
        return JsonSerializer.Serialize(errors); 
    }
    
    private void PopulateErrors()
    {
        var errorsJson = TempData["ModelStateErrors"]?.ToString();
        var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsJson!);

        foreach (var error in errors)
        {
            foreach (var errorMessage in error.Value)
            {
                ModelState.AddModelError(error.Key, errorMessage);
            }
        }
    }
   
}