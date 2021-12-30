using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTO.v1;
using WebApp.API.DTO.v1.AutoMapper.Mappers.Checkout;
using WebApp.API.DTO.v1.Checkout;
using WebApp.API.DTO.v1.Identity;
using WebApp.BLL.Interfaces;

namespace WebApp.Controllers.v1.Checkout;

/// <summary>
/// Controller for managing Checkout payments with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class CheckoutController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly CartMapper _cartMapper;
    private readonly UserManager<AppUser> _userManager;

    /// <inheritdoc />
    public CheckoutController(IAppBLL bll, IMapper mapper, UserManager<AppUser> userManager)
    {
        _bll = bll;
        _cartMapper = new CartMapper(mapper);
        _userManager = userManager;
    }
        
    // POST: api/Checkout/Process
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Process a Checkout payment
    /// </summary>
    /// <param name="cart">Cart details</param>
    /// <returns>Order that was created</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Order>> Process(Cart cart)
    {
        cart.AppUserId = Guid.Parse(_userManager.GetUserId(User));
            
        var order = await _bll.Orders.Process(_cartMapper.Map(cart));
        return Ok(order);
    }
}