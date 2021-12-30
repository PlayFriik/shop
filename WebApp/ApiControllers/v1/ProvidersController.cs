using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTO.v1;
using WebApp.API.DTO.v1.AutoMapper.Mappers;
using WebApp.BLL.Interfaces;

namespace WebApp.Controllers.v1;

/// <summary>
/// Controller for managing Providers with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProvidersController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly ProviderMapper _mapper;

    /// <inheritdoc />
    public ProvidersController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new ProviderMapper(mapper);
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
        var list = await _bll.Providers.ToListAsync();
        var select = list.Select(bllEntity => _mapper.Map(bllEntity));
            
        return Ok(select);
    }

    // GET: api/Providers/5
    /// <summary>
    /// Get an existing Provider by ID
    /// </summary>
    /// <param name="id">Provider ID (GUID)</param>
    /// <returns>Provider that was retrieved by ID</returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Provider), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Provider>> GetProvider(Guid id)
    {
        var provider = await _bll.Providers.FirstOrDefaultAsync(id, true);

        if (provider == null)
        {
            return NotFound();
        }

        return _mapper.Map(provider);
    }

    // PUT: api/Providers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Provider by ID
    /// </summary>
    /// <param name="id">Provider ID (GUID)</param>
    /// <param name="provider">Provider details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id}")]
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

        _bll.Providers.Update(_mapper.Map(provider));
        await _bll.SaveChangesAsync();

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
        provider.Id = _bll.Providers.Add(_mapper.Map(provider)).Id;
            
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetProvider", new { id = provider.Id }, provider);
    }

    // DELETE: api/Providers/5
    /// <summary>
    /// Delete an existing Provider by ID
    /// </summary>
    /// <param name="id">Provider ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteProvider(Guid id)
    {
        var provider = await _bll.Providers.FirstOrDefaultAsync(id, false);
        if (provider == null)
        {
            return NotFound();
        }

        _bll.Providers.Remove(provider);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}