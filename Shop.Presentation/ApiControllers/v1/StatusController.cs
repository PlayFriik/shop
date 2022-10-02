using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Models.v1;
using Shop.Application.Services.v1;

namespace Shop.Presentation.ApiControllers.v1;

/// <summary>
/// Controller for managing Statuses with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    /// <inheritdoc />
    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
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
        return Ok(await _statusService.ToListAsync());
    }

    // GET: api/Statuses/5
    /// <summary>
    /// Get an existing Status by ID
    /// </summary>
    /// <param name="id">Status ID (GUID)</param>
    /// <returns>Status that was retrieved by ID</returns>
    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Status), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Status>> GetStatus(Guid id)
    {
        var status = await _statusService.FirstOrDefaultAsync(id, true);
        if (status == null)
        {
            return NotFound();
        }

        return status;
    }

    // PUT: api/Statuses/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Status by ID
    /// </summary>
    /// <param name="id">Status ID (GUID)</param>
    /// <param name="status">Status details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id:guid}")]
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

        _statusService.Update(status);
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
        status.Id = _statusService.Add(status).Id;
        return CreatedAtAction("GetStatus", new { id = status.Id }, status);
    }

    // DELETE: api/Statuses/5
    /// <summary>
    /// Delete an existing Status by ID
    /// </summary>
    /// <param name="id">Status ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteStatus(Guid id)
    {
        var status = await _statusService.FirstOrDefaultAsync(id, false);
        if (status == null)
        {
            return NotFound();
        }

        _statusService.Remove(status);
        return NoContent();
    }
}