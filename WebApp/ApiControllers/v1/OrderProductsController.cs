using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTO.v1;
using WebApp.API.DTO.v1.AutoMapper.Mappers;
using WebApp.BLL.Interfaces;

namespace WebApp.Controllers.v1;

/// <summary>
/// Controller for managing OrderProducts with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrderProductsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly OrderProductMapper _mapper;

    /// <inheritdoc />
    public OrderProductsController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new OrderProductMapper(mapper);
    }

    // GET: api/OrderProducts
    /// <summary>
    /// Get a list of OrderProducts
    /// </summary>
    /// <returns>List of OrderProducts</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderProduct>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts()
    {
        var list = await _bll.OrderProducts.ToListAsync();
        var select = list.Select(bllEntity => _mapper.Map(bllEntity));
            
        return Ok(select);
    }

    // GET: api/OrderProducts/5
    /// <summary>
    /// Get an existing OrderProduct by ID
    /// </summary>
    /// <param name="id">OrderProduct ID (GUID)</param>
    /// <returns>OrderProduct that was retrieved by ID</returns>
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderProduct), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<OrderProduct>> GetOrderProduct(Guid id)
    {
        var orderProduct = await _bll.OrderProducts.FirstOrDefaultAsync(id, true);

        if (orderProduct == null)
        {
            return NotFound();
        }

        return _mapper.Map(orderProduct);
    }

    // PUT: api/OrderProducts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing OrderProduct by ID
    /// </summary>
    /// <param name="id">OrderProduct ID (GUID)</param>
    /// <param name="orderProduct">OrderProduct details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PutOrderProduct(Guid id, OrderProduct orderProduct)
    {
        if (id != orderProduct.Id)
        {
            return BadRequest();
        }

        _bll.OrderProducts.Update(_mapper.Map(orderProduct));
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/OrderProducts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new OrderProduct
    /// </summary>
    /// <param name="orderProduct">OrderProduct details</param>
    /// <returns>OrderProduct that was added</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderProduct), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<OrderProduct>> PostOrderProduct(OrderProduct orderProduct)
    {
        orderProduct.Id = _bll.OrderProducts.Add(_mapper.Map(orderProduct)).Id;
            
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetOrderProduct", new { id = orderProduct.Id }, orderProduct);
    }

    // DELETE: api/OrderProducts/5
    /// <summary>
    /// Delete an existing OrderProduct by ID
    /// </summary>
    /// <param name="id">OrderProduct ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteOrderProduct(Guid id)
    {
        var orderProduct = await _bll.OrderProducts.FirstOrDefaultAsync(id, false);
        if (orderProduct == null)
        {
            return NotFound();
        }

        _bll.OrderProducts.Remove(orderProduct);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}