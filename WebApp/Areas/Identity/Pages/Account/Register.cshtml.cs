using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WebApp.Domain.Identity;

#pragma warning disable 1591
namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public string? ReturnUrl { get; set; }

        public IList<AuthenticationScheme>? ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [Display(Name = nameof(FirstName), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Register))]
            [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            public string FirstName { get; set; } = default!;
            
            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [Display(Name = nameof(LastName), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Register))]
            [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            public string LastName { get; set; } = default!;

            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [EmailAddress]
            [Display(Name = nameof(Email), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Register))]
            public string Email { get; set; } = default!;

            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [Phone]
            [Display(Name = nameof(PhoneNumber), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Manage.Index))]
            public string PhoneNumber { get; set; } = default!;

            [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = "ErrorMessage_StringLengthMinMax", ErrorMessageResourceType = typeof(Base.Resources.Common))]
            [DataType(DataType.Password)]
            [Display(Name = nameof(Password), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Register))]
            public string Password { get; set; } = default!;

            [DataType(DataType.Password)]
            [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Register))]
            [Compare("Password", ErrorMessageResourceName = "PasswordsDontMatch", ErrorMessageResourceType = typeof(Base.Resources.WebApp.Areas.Identity.Pages.Account.Register))]
            public string ConfirmPassword { get; set; } = default!;
        }

        public async Task OnGetAsync(string? returnUrl)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = Url.Content("~/")!;
            }
            
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };
                
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
