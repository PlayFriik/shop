using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Models.v1.Identity;
using Shop.Presentation.Extensions;

namespace Shop.Presentation.ApiControllers.v1.Identity;

/// <summary>
/// Controller for managing AppUsers with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly SignInManager<Shop.Domain.Models.Identity.AppUser> _signInManager;
    private readonly UserManager<Shop.Domain.Models.Identity.AppUser> _userManager;

    /// <inheritdoc />
    public UserController(
        IConfiguration configuration,
        SignInManager<Shop.Domain.Models.Identity.AppUser> signInManager,
        UserManager<Shop.Domain.Models.Identity.AppUser> userManager)
    {
        _configuration = configuration;

        _signInManager = signInManager;
        _userManager = userManager;
    }

    /// <summary>
    /// Login an existing AppUser
    /// </summary>
    /// <param name="loginRequest">Login details</param>
    /// <returns>LoginResponse that contains AppUser details</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AppUser.LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppUser.LoginResponse>> Login([FromBody] AppUser.LoginRequest loginRequest)
    {
        var appUser = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (appUser == null)
        {
            return BadRequest(new Message("Email or password is incorrect"));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginRequest.Password, false);
        if (!result.Succeeded) return BadRequest(new Message("Email or password is incorrect"));

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        var jwt = claimsPrincipal.CreateJwt(_configuration);

        return Ok(new AppUser.LoginResponse
        {
            Token = jwt,
            Email = appUser.Email,
            FullName = appUser.FirstName + " " + appUser.LastName,
            PhoneNumber = appUser.PhoneNumber,
            Roles = (await _userManager.GetRolesAsync(appUser)).ToList()
        });
    }

    /// <summary>
    /// Register a new AppUser
    /// </summary>
    /// <param name="registerRequest">Register details</param>
    /// <returns>RegisterResponse that contains AppUser details</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AppUser.RegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppUser.RegisterResponse>> Register([FromBody] AppUser.RegisterRequest registerRequest)
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