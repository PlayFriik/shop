using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Models.v1;
using Shop.Application.Services.v1;

namespace Shop.Presentation.ApiControllers.v1;

/// <summary>
/// Controller for managing Orders with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly UserManager<Shop.Domain.Models.Identity.AppUser> _userManager;

    /// <inheritdoc />
    public OrderController(IOrderService orderService, UserManager<Shop.Domain.Models.Identity.AppUser> userManager)
    {
        _orderService = orderService;
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
        return Ok(User.IsInRole("Admin") || User.IsInRole("Seller")
            ? await _orderService.ToListAsync()
            : await _orderService.ToListAsync(Guid.Parse(_userManager.GetUserId(User))));
    }

    // GET: api/Orders/5
    /// <summary>
    /// Get an existing Order by ID
    /// </summary>
    /// <param name="id">Order ID (GUID)</param>
    /// <returns>Order that was retrieved by ID</returns>
    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Order>> GetOrder(Guid id)
    {
        var order = User.IsInRole("Admin") || User.IsInRole("Seller")
            ? await _orderService.FirstOrDefaultAsync(id, true)
            : await _orderService.FirstOrDefaultAsync(id, true, Guid.Parse(_userManager.GetUserId(User)));
        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    // PUT: api/Orders/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Order by ID
    /// </summary>
    /// <param name="id">Order ID (GUID)</param>
    /// <param name="order">Order details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id:guid}")]
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

        _orderService.Update(order);
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
        order.Id = _orderService.Add(order).Id;
        return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }

    // DELETE: api/Orders/5
    /// <summary>
    /// Delete an existing Order by ID
    /// </summary>
    /// <param name="id">Order ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var order = await _orderService.FirstOrDefaultAsync(id, false);
        if (order == null)
        {
            return NotFound();
        }

        _orderService.Remove(order);
        return NoContent();
    }
}