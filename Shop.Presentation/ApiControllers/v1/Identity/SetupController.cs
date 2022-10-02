using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Models.v1.Identity;
using Shop.Presentation.Extensions;

namespace Shop.Presentation.ApiControllers.v1.Identity;

/// <summary>
/// Controller for setting up initial administrator with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SetupController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly SignInManager<Shop.Domain.Models.Identity.AppUser> _signInManager;
    private readonly UserManager<Shop.Domain.Models.Identity.AppUser> _userManager;

    /// <inheritdoc />
    public SetupController(
        IConfiguration configuration,
        SignInManager<Shop.Domain.Models.Identity.AppUser> signInManager,
        UserManager<Shop.Domain.Models.Identity.AppUser> userManager)
    {
        _configuration = configuration;

        _signInManager = signInManager;
        _userManager = userManager;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="registerRequest">Register details</param>
    /// <returns>RegisterResponse that contains JWT token, email and full name</returns>
    [HttpPost]
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AppUser.RegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppUser.RegisterResponse>> Setup([FromBody] AppUser.RegisterRequest registerRequest)
    {
        var appUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (appUser != null)
        {
            return BadRequest(new Message("User has already registered"));
        }

        appUser = new Shop.Domain.Models.Identity.AppUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.PhoneNumber,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName
        };

        var result = await _userManager.CreateAsync(appUser, registerRequest.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new Message
            {
                Messages = errors
            });
        }

        var user = await _userManager.FindByEmailAsync(appUser.Email);
        if (user == null) return BadRequest(new Message("An error occurred while registering user"));

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        var jwt = claimsPrincipal.CreateJwt(_configuration);

        return Ok(new AppUser.RegisterResponse
        {
            Token = jwt,
            Email = appUser.Email,
            FullName = appUser.FirstName + " " + appUser.LastName,
            PhoneNumber = appUser.PhoneNumber,
            Roles = (await _userManager.GetRolesAsync(appUser)).ToList()
        });
    }
}