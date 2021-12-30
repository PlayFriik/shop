using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTO.v1;
using WebApp.API.DTO.v1.AutoMapper.Mappers;
using WebApp.BLL.Interfaces;

namespace WebApp.Controllers.v1;

/// <summary>
/// Controller for managing Orders with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly OrderMapper _mapper;
    private readonly UserManager<WebApp.Domain.Identity.AppUser> _userManager;

    /// <inheritdoc />
    public OrdersController(IAppBLL bll, IMapper mapper, UserManager<WebApp.Domain.Identity.AppUser> userManager)
    {
        _bll = bll;
        _mapper = new OrderMapper(mapper);
        _userManager = userManager;
    }

    // GET: api/Orders
    /// <summary>
    /// Get a list of Orders
    /// </summary>
    /// <returns>List of Orders</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var list = User.IsInRole("Admin") || User.IsInRole("Seller")
            ? await _bll.Orders.ToListAsync()
            : await _bll.Orders.ToListAsync(Guid.Parse(_userManager.GetUserId(User)));
            
        var select = list.Select(bllEntity => _mapper.Map(bllEntity));
            
        return Ok(select);
    }

    // GET: api/Orders/5
    /// <summary>
    /// Get an existing Order by ID
    /// </summary>
    /// <param name="id">Order ID (GUID)</param>
    /// <returns>Order that was retrieved by ID</returns>
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Order>> GetOrder(Guid id)
    {
        var order = User.IsInRole("Admin") || User.IsInRole("Seller")
            ? await _bll.Orders.FirstOrDefaultAsync(id, true)
            : await _bll.Orders.FirstOrDefaultAsync(id, true, Guid.Parse(_userManager.GetUserId(User)));
        if (order == null)
        {
            return NotFound();
        }

        return _mapper.Map(order);
    }

    // PUT: api/Orders/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Order by ID
    /// </summary>
    /// <param name="id">Order ID (GUID)</param>
    /// <param name="order">Order details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PutOrder(Guid id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        _bll.Orders.Update(_mapper.Map(order));
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Orders
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new Order
    /// </summary>
    /// <param name="order">Order details</param>
    /// <returns>Order that was added</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Order>> PostOrder(Order order)
    {
        order.Id = _bll.Orders.Add(_mapper.Map(order)).Id;
            
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }

    // DELETE: api/Orders/5
    /// <summary>
    /// Delete an existing Order by ID
    /// </summary>
    /// <param name="id">Order ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var order = await _bll.Orders.FirstOrDefaultAsync(id, false);
        if (order == null)
        {
            return NotFound();
        }

        _bll.Orders.Remove(order);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}