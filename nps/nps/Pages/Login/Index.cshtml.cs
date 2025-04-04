using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models.DTOS;

namespace nps.Pages.Login
{
    public class IndexModel : PageModel
    {
		[BindProperty]

        public required LoginInfo LoginInfo { get; set; }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
			return RedirectToPage("/Index");
		}
    }
}
