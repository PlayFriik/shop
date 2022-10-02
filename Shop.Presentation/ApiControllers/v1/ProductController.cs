using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Models.v1;
using Shop.Application.Services.v1;

namespace Shop.Presentation.ApiControllers.v1;

/// <summary>
/// Controller for managing Products with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    /// <inheritdoc />
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/Products
    /// <summary>
    /// Get a list of Products
    /// </summary>
    /// <returns>List of Products</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] Guid? category, [FromQuery] string? sortBy)
    {
        return Ok(await _productService.ToListAsync(category, sortBy));
    }

    // GET: api/Products/5
    /// <summary>
    /// Get an existing Product by ID
    /// </summary>
    /// <param name="id">Product ID (GUID)</param>
    /// <returns>Product that was retrieved by ID</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> GetProduct(Guid id)
    {
        var product = await _productService.FirstOrDefaultAsync(id, true);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // PUT: api/Products/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Product by ID
    /// </summary>
    /// <param name="id">Product ID (GUID)</param>
    /// <param name="product">Product details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PutProduct(Guid id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _productService.Update(product);
        return NoContent();
    }

    // POST: api/Products
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new Product
    /// </summary>
    /// <param name="product">Product details</param>
    /// <returns>Product that was added</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        product.Id = _productService.Add(product).Id;
        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // DELETE: api/Products/5
    /// <summary>
    /// Delete an existing Product by ID
    /// </summary>
    /// <param name="id">Product ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _productService.FirstOrDefaultAsync(id, false);
        if (product == null)
        {
            return NotFound();
        }

        _productService.Remove(product);
        return NoContent();
    }
}