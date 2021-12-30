using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTO.v1;
using WebApp.API.DTO.v1.AutoMapper.Mappers;
using WebApp.BLL.Interfaces;

namespace WebApp.Controllers.v1;

/// <summary>
/// Controller for managing Transactions with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IAppBLL _bll;
    private readonly TransactionMapper _mapper;
    private readonly UserManager<WebApp.Domain.Identity.AppUser> _userManager;

    /// <inheritdoc />
    public TransactionsController(IAppBLL bll, IMapper mapper, UserManager<WebApp.Domain.Identity.AppUser> userManager)
    {
        _bll = bll;
        _mapper = new TransactionMapper(mapper);
        _userManager = userManager;
    }

    // GET: api/Transactions
    /// <summary>
    /// Get a list of Transactions
    /// </summary>
    /// <returns>List of Transactions</returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Transaction>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        var list = await _bll.Transactions.ToListAsync(Guid.Parse(_userManager.GetUserId(User)));
        var select = list.Select(bllEntity => _mapper.Map(bllEntity));
            
        return Ok(select);
    }

    // GET: api/Transactions/5
    /// <summary>
    /// Get an existing Transaction by ID
    /// </summary>
    /// <param name="id">Transaction ID (GUID)</param>
    /// <returns>Transaction that was retrieved by ID</returns>
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Transaction), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Transaction>> GetTransaction(Guid id)
    {
        var transaction = await _bll.Transactions.FirstOrDefaultAsync(id, true, Guid.Parse(_userManager.GetUserId(User)));

        if (transaction == null)
        {
            return NotFound();
        }

        return _mapper.Map(transaction);
    }

    // PUT: api/Transactions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Transaction by ID
    /// </summary>
    /// <param name="id">Transaction ID (GUID)</param>
    /// <param name="transaction">Transaction details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PutTransaction(Guid id, Transaction transaction)
    {
        if (id != transaction.Id)
        {
            return BadRequest();
        }

        _bll.Transactions.Update(_mapper.Map(transaction));
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Transactions
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new Transaction
    /// </summary>
    /// <param name="transaction">Transaction details</param>
    /// <returns>Transaction that was added</returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Transaction), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
    {
        transaction.Id = _bll.Transactions.Add(_mapper.Map(transaction)).Id;
            
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
    }

    // DELETE: api/Transactions/5
    /// <summary>
    /// Delete an existing Transaction by ID
    /// </summary>
    /// <param name="id">Transaction ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        var transaction = await _bll.Transactions.FirstOrDefaultAsync(id, false, Guid.Parse(_userManager.GetUserId(User)));
        if (transaction == null)
        {
            return NotFound();
        }

        _bll.Transactions.Remove(transaction);
        await _bll.SaveChangesAsync();

        return NoContent();
    }
}