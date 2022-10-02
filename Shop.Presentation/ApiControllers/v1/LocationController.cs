using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Models.v1;
using Shop.Application.Services.v1;

namespace Shop.Presentation.ApiControllers.v1;

/// <summary>
/// Controller for managing Locations with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    /// <inheritdoc />
    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    // GET: api/Locations
    /// <summary>
    /// Get a list of Locations
    /// </summary>
    /// <returns>List of Locations</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Location>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Location>>> GetLocations(Guid? providerId)
    {
        var locations = await _locationService.ToListAsync();
            
        if (providerId != null)
        {
            locations = locations.Where(location => location.ProviderId == providerId);
        }

        return Ok(locations);
    }

    // GET: api/Locations/5
    /// <summary>
    /// Get an existing Location by ID
    /// </summary>
    /// <param name="id">Location ID (GUID)</param>
    /// <returns>Location that was retrieved by ID</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Location), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Location>> GetLocation(Guid id)
    {
        var location = await _locationService.FirstOrDefaultAsync(id, true);

        if (location == null)
        {
            return NotFound();
        }

        return location;
    }

    // PUT: api/Locations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Location by ID
    /// </summary>
    /// <param name="id">Location ID (GUID)</param>
    /// <param name="location">Location details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PutLocation(Guid id, Location location)
    {
        if (id != location.Id)
        {
            return BadRequest();
        }

        _locationService.Update(location);
        return NoContent();
    }

    // POST: api/Locations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new Location
    /// </summary>
    /// <param name="location">Location details</param>
    /// <returns>Location that was added</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Location), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Location>> PostLocation(Location location)
    {
        location.Id = _locationService.Add(location).Id;
        return CreatedAtAction("GetLocation", new { id = location.Id }, location);
    }

    // DELETE: api/Locations/5
    /// <summary>
    /// Delete an existing Location by ID
    /// </summary>
    /// <param name="id">Location ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteLocation(Guid id)
    {
        var location = await _locationService.FirstOrDefaultAsync(id, false);
        if (location == null)
        {
            return NotFound();
        }

        _locationService.Remove(location);
        return NoContent();
    }
}