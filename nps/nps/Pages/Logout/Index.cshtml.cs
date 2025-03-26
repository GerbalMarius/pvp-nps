using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models;

namespace nps.Pages.Logout
{
	public class IndexModel : PageModel
	{
		public IActionResult OnPost()
		{
			HttpContext.Session.Clear();
			return RedirectToPage("/Index");
		}
	}
}
