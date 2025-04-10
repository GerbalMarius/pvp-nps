using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models.DTOS;

namespace nps.Pages.Login
{
	public class IndexModel : PageModel
	{

		[BindProperty]

		public required LoginInfo LoginInfo { get; set; }
        public string? Email { get; set; }

        public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			HttpContext.Session.Add("email", LoginInfo.Email);

			Email = HttpContext.Session.Get<string>("email");

			return  RedirectToPage("/Index");
		}
	}
}
