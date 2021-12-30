using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTO.v1;
using WebApp.API.DTO.v1.AutoMapper.Mappers;
using WebApp.BLL.Interfaces;

namespace WebApp.Controllers.v1;

/// <summary>
/// Controller for managing Pictures with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PicturesController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly PictureMapper _mapper;

    /// <inheritdoc />
    public PicturesController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new PictureMapper(mapper);
    }

    // GET: api/Pictures
    /// <summary>
    /// Get a list of Pictures
    /// </summary>
    /// <returns>List of Pictures</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Picture>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Picture>>> GetPictures()
    {
        var list = await _bll.Pictures.ToListAsync();
        var select = list.Select(bllEntity => _mapper.Map(bllEntity));
            
        return Ok(select);
    }

    // GET: api/Pictures/5
    /// <summary>
    /// Get an existing Picture by ID
    /// </summary>
    /// <param name="id">Picture ID (GUID)</param>
    /// <returns>Picture that was retrieved by ID</returns>
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Picture), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Picture>> GetPicture(Guid id)
    {
        var picture = await _bll.Pictures.FirstOrDefaultAsync(id, true);

        if (picture == null)
        {
            return NotFound();
        }

        return _mapper.Map(picture);
    }

    // PUT: api/Pictures/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Picture by ID
    /// </summary>
    /// <param name="id">Picture ID (GUID)</param>
    /// <param name="picture">Picture details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> PutPicture(Guid id, Picture picture)
    {
        if (id != picture.Id)
        {
            return BadRequest();
        }

        _bll.Pictures.Update(_mapper.Map(picture));
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Pictures
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new Picture
    /// </summary>
    /// <param name="picture">Picture details</param>
    /// <returns>Picture that was added</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Picture), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Picture>> PostPicture(Picture picture)
    {
        picture.Id = _bll.Pictures.Add(_mapper.Map(picture)).Id;
            
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetPicture", new { id = picture.Id }, picture);
    }

    // DELETE: api/Pictures/5
    /// <summary>
    /// Delete an existing Picture by ID
    /// </summary>
    /// <param name="id">Picture ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeletePicture(Guid id)
    {
        var picture = await _bll.Pictures.FirstOrDefaultAsync(id, false);
        if (picture == null)
        {
            return NotFound();
        }

        _bll.Pictures.Remove(picture);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}