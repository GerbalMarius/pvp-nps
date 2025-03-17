using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nps.Pages.Logout
{
	public class IndexModel : PageModel
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		// ? FIX: Constructor should match the class name (not `_logoutModel`)
		public IndexModel(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			// ? FIX: Ensure HttpContext is not null
			if (_httpContextAccessor.HttpContext != null)
			{
				// Remove authentication data
				await _httpContextAccessor.HttpContext.SignOutAsync();

				// Clear session
				_httpContextAccessor.HttpContext.Session.Clear();
			}

			return RedirectToPage("/Login/Index"); // Redirect to login page
		}
	}
}
