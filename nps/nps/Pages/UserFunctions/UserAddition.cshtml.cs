using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models;

namespace nps.Pages.AddUser
{
    public class UserAdditionModel : PageModel
    {
        [BindProperty]
        public UserInfo UserInfo { get; set; }

        public List<UserInfo> UserInfoList = new List<UserInfo>();    
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            UserInfoList.Add(new UserInfo { Email = UserInfo.Email, Password = UserInfo.Password });

            return Page();
        }
    }
}
