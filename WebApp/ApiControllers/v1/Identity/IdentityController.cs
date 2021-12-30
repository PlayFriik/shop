using AutoMapper;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.API.DTO.v1.AutoMapper.Mappers.Identity;
using WebApp.API.DTO.v1.Identity;

namespace WebApp.Controllers.v1.Identity;

/// <summary>
/// Controller for managing Users with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class IdentityController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<IdentityController> _logger;
    private readonly AppUserMapper _mapper;
        
    private readonly SignInManager<WebApp.Domain.Identity.AppUser> _signInManager;
    private readonly UserManager<WebApp.Domain.Identity.AppUser> _userManager;

    /// <inheritdoc />
    public IdentityController(IConfiguration configuration, ILogger<IdentityController> logger, IMapper mapper,
        SignInManager<WebApp.Domain.Identity.AppUser> signInManager, UserManager<WebApp.Domain.Identity.AppUser> userManager)
    {
        _configuration = configuration;
        _logger = logger;
        _mapper = new AppUserMapper(mapper);
            
        _signInManager = signInManager;
        _userManager = userManager;
    }
        
    // GET: api/Identity/AppUsers
    /// <summary>
    /// Get a list of AppUsers
    /// </summary>
    /// <returns>List of AppUsers</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<WebApp.API.DTO.v1.Identity.AppUser>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<WebApp.API.DTO.v1.Identity.AppUser>>> AppUsers()
    {
        var list = await _userManager.Users.ToListAsync();
        var select = list.Select(domainEntity => _mapper.Map(domainEntity));
            
        return Ok(select);
    }

    /// <summary>
    /// Login an existing user
    /// </summary>
    /// <param name="loginRequest">Login details</param>
    /// <returns>LoginResponse that contains JWT token, email and full name</returns>
    [HttpPost]
    [AllowAnonymous]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
    {
        var appUser = await _userManager.FindByEmailAsync(loginRequest.Email);
        if (appUser == null)
        {
            _logger.LogWarning("User {Email} not found", loginRequest.Email);
            return BadRequest(new Message("Email or password is incorrect"));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginRequest.Password, false);
        if (result.Succeeded)
        {
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
            var token = IdentityExtension.GenerateJwt(
                claimsPrincipal.Claims,
                _configuration.GetValue<string>("JWT:Key"),
                _configuration.GetValue<string>("JWT:Issuer"),
                _configuration.GetValue<string>("JWT:Issuer"),
                DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
            );
                
            _logger.LogInformation("User {Email} has logged in", loginRequest.Email);
            return Ok(new LoginResponse
            {
                Token = token,
                Email = appUser.Email,
                FullName = appUser.FirstName + " " + appUser.LastName,
                PhoneNumber = appUser.PhoneNumber,
                Roles = (await _userManager.GetRolesAsync(appUser)).ToList()
            });
        }
            
        _logger.LogWarning("Wrong password for user {Email}", loginRequest.Email);
        return BadRequest(new Message("Email or password is incorrect"));
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
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest registerRequest)
    {
        var appUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User {Email} has already registered", registerRequest.Email);
            return BadRequest(new Message("User has already registered"));
        }

        appUser = new WebApp.Domain.Identity.AppUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.PhoneNumber,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName
        };
            
        var result = await _userManager.CreateAsync(appUser, registerRequest.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("User {Email} has created an account with password", appUser.Email);
                
            var user = await _userManager.FindByEmailAsync(appUser.Email);
            if (user != null)
            {                
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                var token = IdentityExtension.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration.GetValue<string>("JWT:Key"),
                    _configuration.GetValue<string>("JWT:Issuer"),
                    _configuration.GetValue<string>("JWT:Issuer"),
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                );
                    
                _logger.LogInformation("User {Email} has logged in", registerRequest.Email);
                return Ok(new RegisterResponse
                {
                    Token = token,
                    Email = appUser.Email,
                    FullName = appUser.FirstName + " " + appUser.LastName,
                    PhoneNumber = appUser.PhoneNumber,
                    Roles = (await _userManager.GetRolesAsync(appUser)).ToList()
                });
                    
            }
            else
            {
                _logger.LogInformation("User {Email} not found after creation", appUser.Email);
                return BadRequest(new Message("User not found after creation"));
            }
        }
            
        var errors = result.Errors.Select(error => error.Description).ToList();
        return BadRequest(new Message
        {
            Messages = errors
        });
    }
}