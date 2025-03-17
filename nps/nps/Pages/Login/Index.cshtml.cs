using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using nps.Models;

namespace nps.Pages.Login
{
	public class IndexModel : PageModel
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public IndexModel(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		[BindProperty]

		public required LoginInfo LoginInfo { get; set; }
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			// Note : Sutvarkyti pilnai kai bus db
			if (LoginInfo.Email == "admin@gmail.com" && LoginInfo.Password == "password123!")
			{
				var userId = "12345";

				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, LoginInfo.Email),
					new Claim("UserId", userId)
				};

				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var authProperties = new AuthenticationProperties
				{
					IsPersistent = true,
					ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProperties
				);

				_httpContextAccessor.HttpContext?.Session.SetString("UserId", userId);


				return RedirectToPage("/Index");
			}

			ModelState.AddModelError(string.Empty, "Invalid email or password");
			return Page();
		}
	}
}
