using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Domain.Identity;

#pragma warning disable 1591
namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = nameof(Username), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index))]
        public string? Username { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public class InputModel
        {
            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [Display(Name = nameof(FirstName), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index))]
            [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            public string FirstName { get; set; } = default!;
            
            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [Display(Name = nameof(LastName), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index))]
            [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            public string LastName { get; set; } = default!;
            
            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [Phone]
            [Display(Name = nameof(PhoneNumber), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index))]
            public string PhoneNumber { get; set; } = default!;
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index.Unexpected_error_when_trying_to_set_phone_number, _userManager.GetUserId(User)));
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index.Unexpected_error_when_trying_to_set_phone_number, _userManager.GetUserId(User)));
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index.Unexpected_error_when_trying_to_set_phone_number;
                    return RedirectToPage();
                }
            }

            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded)
            {
                StatusMessage = Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index.Unexpected_error_when_trying_to_update_user;
                return RedirectToPage();
            }
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index.Your_profile_has_been_updated;
            return RedirectToPage();
        }
    }
}
