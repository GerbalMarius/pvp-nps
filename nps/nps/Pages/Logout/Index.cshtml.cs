using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nps.Pages.Logout
{
	public class IndexModel : PageModel
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public IndexModel(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<IActionResult> OnGetAsync()
		{

			if (_httpContextAccessor.HttpContext != null)
			{

				await _httpContextAccessor.HttpContext.SignOutAsync();

				_httpContextAccessor.HttpContext.Session.Clear();
			}

			return RedirectToPage("/Login/Index");
		}
	}
}
