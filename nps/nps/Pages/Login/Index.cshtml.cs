using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using nps.Models;

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
