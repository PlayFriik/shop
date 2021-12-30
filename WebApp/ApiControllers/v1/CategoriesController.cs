using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTO.v1;
using WebApp.API.DTO.v1.AutoMapper.Mappers;
using WebApp.BLL.Interfaces;

namespace WebApp.Controllers.v1;

/// <summary>
/// Controller for managing Categories with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly CategoryMapper _mapper;

    /// <inheritdoc />
    public CategoriesController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new CategoryMapper(mapper);
    }

    // GET: api/Categories
    /// <summary>
    /// Get a list of Categories
    /// </summary>
    /// <returns>List of Categories</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var list = await _bll.Categories.ToListAsync();
        var select = list.Select(bllEntity => _mapper.Map(bllEntity));
            
        return Ok(select);
    }

    // GET: api/Categories/5
    /// <summary>
    /// Get an existing Category by ID
    /// </summary>
    /// <param name="id">Category ID (GUID)</param>
    /// <returns>Category that was retrieved by ID</returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        var category = await _bll.Categories.FirstOrDefaultAsync(id, true);

        if (category == null)
        {
            return NotFound();
        }

        return _mapper.Map(category);
    }

    // PUT: api/Categories/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Category by ID
    /// </summary>
    /// <param name="id">Category ID (GUID)</param>
    /// <param name="category">Category details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PutCategory(Guid id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        _bll.Categories.Update(_mapper.Map(category));
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Categories
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new Category
    /// </summary>
    /// <param name="category">Category details</param>
    /// <returns>Category that was added</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Category>> PostCategory(Category category)
    {
        category.Id = _bll.Categories.Add(_mapper.Map(category)).Id;
            
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetCategory", new { id = category.Id }, category);
    }

    // DELETE: api/Categories/5
    /// <summary>
    /// Delete an existing Category by ID
    /// </summary>
    /// <param name="id">Category ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var category = await _bll.Categories.FirstOrDefaultAsync(id, false);
        if (category == null)
        {
            return NotFound();
        }

        _bll.Categories.Remove(category);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}