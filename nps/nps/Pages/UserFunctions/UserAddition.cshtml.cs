using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nps.Models;

namespace nps.Pages.UserFunctions
{
    public class UserAdditionModel : PageModel
    {
        [BindProperty]
        public UserInfo UserInfo { get; set; }

        //Better to keep immutable for outside access
        //public List<UserInfo> UserInfoList { get; init; } = [];
        // since .Net 8.0 all collections that implement IEnumerable<T>, except for IDictionary<TKey, TValue>,
        // can be initialised using "collection expression" https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/collection-expressions
        public List<UserInfo> UserInfoList = new List<UserInfo>();  
        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //for this to work the data transfer model should've been anotaded with annotations defined in System.Component.Model.DataAnnotations library
            //for example [Required], [MaxLength] etc.
            if(!ModelState.IsValid)
            {
                return Page();
            }

            UserInfoList.Add(new UserInfo { Email = UserInfo.Email, Password = UserInfo.Password });

            return Page();
        }
    }
}
