using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models;
using nps.Models.DTOS;
using nps.Services.Worker;

namespace nps.Pages.Auth;

public class Index : PageModel
{
    private readonly ILogger<Index> _logger;
    private readonly IWorkerService _workerService;

    public Index(ILogger<Index> logger, IWorkerService workerService)
    {
        _logger = logger;
        _workerService = workerService;
    }


    private const string Errors = "ModelStateErrors";
    
    [BindProperty]
    public LoginInfo LoginData { get; set; } = new();
    
    [BindProperty]
    public RegisterInfo RegisterData { get; set; } = new();
    
    public IActionResult OnGet()
    {
        if (TempData[Errors] == null) return Page();
        
        PopulateErrors();

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var formType = Request.Form["FormType"];

        if (formType == "Login")
        {
            ClearModelStateIf(key => !key.Contains(nameof(LoginData)));

            if (!ModelState.IsValid)
            {
                TempData[Errors] = KeepErrors(ModelState);
                return RedirectToPage("/Auth/Index", new { errorType = "Login" });
                
            }
            var user = await _workerService.GetByEmail(LoginData.Email);

            if(user == null || !_workerService.PasswordMatch(LoginData.Password, user.Password) /*|| !_workerService.PasswordMatchHASH(LoginData.Password, user.Password)*/)
            {
                ModelState.AddModelError(string.Empty, "Wrong email or password");
                TempData[Errors] = KeepErrors(ModelState);
                return RedirectToPage("/Auth/Index", new { errorType = "Login" });
            }

            TempData[Errors] = null;
            return RedirectToPage("/Dashboard/Index");
        }

        ClearModelStateIf(key => !key.Contains(nameof(RegisterData)));

        if (!ModelState.IsValid)
        {
            TempData[Errors] = KeepErrors(ModelState); 
            return RedirectToPage("/Auth/Index", new { errorType = "Register" });
        }
        TempData[Errors] = null;
        return Page();
                
    }

    private void ClearModelStateIf(Func<string, bool> predicate)
    {
        foreach (var key in ModelState.Keys.Where(predicate))
        {
            _logger.LogInformation("{key}", key);
            ModelState.Remove(key);
        }
    }

    private string KeepErrors(ModelStateDictionary modelState)
    {
        var errors = modelState.ToDictionary(
            entry => entry.Key,
            entry => entry.Value.Errors.Select(e => e.ErrorMessage).ToArray()
        );
        return JsonSerializer.Serialize(errors); 
    }
    
    private void PopulateErrors()
    {
        var errorsJson = TempData[Errors]?.ToString();
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