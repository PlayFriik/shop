using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Models.v1;
using Shop.Application.Services.v1;

namespace Shop.Presentation.ApiControllers.v1;

/// <summary>
/// Controller for managing Providers with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;

    /// <inheritdoc />
    public ProviderController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    // GET: api/Providers
    /// <summary>
    /// Get a list of Providers
    /// </summary>
    /// <returns>List of Providers</returns>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Provider>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Provider>>> GetProviders()
    {
        return Ok(await _providerService.ToListAsync());
    }

    // GET: api/Providers/5
    /// <summary>
    /// Get an existing Provider by ID
    /// </summary>
    /// <param name="id">Provider ID (GUID)</param>
    /// <returns>Provider that was retrieved by ID</returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Provider), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Provider>> GetProvider(Guid id)
    {
        var provider = await _providerService.FirstOrDefaultAsync(id, true);

        if (provider == null)
        {
            return NotFound();
        }

        return provider;
    }

    // PUT: api/Providers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Provider by ID
    /// </summary>
    /// <param name="id">Provider ID (GUID)</param>
    /// <param name="provider">Provider details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PutProvider(Guid id, Provider provider)
    {
        if (id != provider.Id)
        {
            return BadRequest();
        }

        _providerService.Update(provider);
        return NoContent();
    }

    // POST: api/Providers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new Provider
    /// </summary>
    /// <param name="provider">Provider details</param>
    /// <returns>Provider that was added</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Provider), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Provider>> PostProvider(Provider provider)
    {
        provider.Id = _providerService.Add(provider).Id;
        return CreatedAtAction("GetProvider", new { id = provider.Id }, provider);
    }

    // DELETE: api/Providers/5
    /// <summary>
    /// Delete an existing Provider by ID
    /// </summary>
    /// <param name="id">Provider ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteProvider(Guid id)
    {
        var provider = await _providerService.FirstOrDefaultAsync(id, false);
        if (provider == null)
        {
            return NotFound();
        }

        _providerService.Remove(provider);
        return NoContent();
    }
}