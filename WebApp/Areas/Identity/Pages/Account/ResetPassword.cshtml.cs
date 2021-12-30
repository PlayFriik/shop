using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WebApp.Domain.Identity;

#pragma warning disable 1591
namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public class InputModel
        {
            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [EmailAddress]
            [Display(Name = nameof(Email), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.ResetPassword))]
            public string? Email { get; set; }

            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "ErrorMessage_StringLengthMinMax", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [DataType(DataType.Password)]
            [Display(Name = nameof(Password), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.ResetPassword))]
            public string? Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.ResetPassword))]
            [Compare("Password", ErrorMessageResourceName = "PasswordsDontMatch", ErrorMessageResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.ResetPassword))]
            public string? ConfirmPassword { get; set; }

            public string? Code { get; set; }
        }

        public IActionResult OnGet(string code = default!)
        {
            if (code.Equals(default))
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
