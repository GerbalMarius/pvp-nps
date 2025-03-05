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

        public required UserInfo UserInfo { get; set; }
        public IActionResult OnPost()
        {
			return RedirectToPage("/Index");
		}
    }
}
