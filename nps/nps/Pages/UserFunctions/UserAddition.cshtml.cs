using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models.DTOS;

namespace nps.Pages.UserFunctions
{
    public class UserAdditionModel : PageModel
    {
        [BindProperty]
        public LoginInfo LoginInfo { get; set; }
        
        public List<LoginInfo> UserInfoList = new List<LoginInfo>();  
        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            UserInfoList.Add(new LoginInfo { Email = LoginInfo.Email, Password = LoginInfo.Password });

            return Page();
        }
    }
}
