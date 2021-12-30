using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTO.v1;
using WebApp.API.DTO.v1.AutoMapper.Mappers;
using WebApp.BLL.Interfaces;

namespace WebApp.Controllers.v1;

/// <summary>
/// Controller for managing Statuses with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class StatusesController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly StatusMapper _mapper;

    /// <inheritdoc />
    public StatusesController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new StatusMapper(mapper);
    }

    // GET: api/Statuses
    /// <summary>
    /// Get a list of Statuses
    /// </summary>
    /// <returns>List of Statuses</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Status>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
    {
        var list = await _bll.Statuses.ToListAsync();
        var select = list.Select(bllEntity => _mapper.Map(bllEntity));
            
        return Ok(select);
    }

    // GET: api/Statuses/5
    /// <summary>
    /// Get an existing Status by ID
    /// </summary>
    /// <param name="id">Status ID (GUID)</param>
    /// <returns>Status that was retrieved by ID</returns>
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Status), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Status>> GetStatus(Guid id)
    {
        var status = await _bll.Statuses.FirstOrDefaultAsync(id, true);
        if (status == null)
        {
            return NotFound();
        }

        return _mapper.Map(status);
    }

    // PUT: api/Statuses/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Status by ID
    /// </summary>
    /// <param name="id">Status ID (GUID)</param>
    /// <param name="status">Status details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PutStatus(Guid id, Status status)
    {
        if (id != status.Id)
        {
            return BadRequest();
        }

        _bll.Statuses.Update(_mapper.Map(status));
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Statuses
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new Status
    /// </summary>
    /// <param name="status">Status details</param>
    /// <returns>Status that was added</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Status), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Status>> PostStatus(Status status)
    {
        status.Id = _bll.Statuses.Add(_mapper.Map(status)).Id;
            
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetStatus", new { id = status.Id }, status);
    }

    // DELETE: api/Statuses/5
    /// <summary>
    /// Delete an existing Status by ID
    /// </summary>
    /// <param name="id">Status ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteStatus(Guid id)
    {
        var status = await _bll.Statuses.FirstOrDefaultAsync(id, false);
        if (status == null)
        {
            return NotFound();
        }

        _bll.Statuses.Remove(status);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}