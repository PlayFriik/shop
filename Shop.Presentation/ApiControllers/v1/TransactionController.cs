using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Models.v1;
using Shop.Application.Services.v1;

namespace Shop.Presentation.ApiControllers.v1;

/// <summary>
/// Controller for managing Transactions with API requests
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly UserManager<Shop.Domain.Models.Identity.AppUser> _userManager;

    /// <inheritdoc />
    public TransactionController(ITransactionService transactionService, UserManager<Shop.Domain.Models.Identity.AppUser> userManager)
    {
        _transactionService = transactionService;
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
        return Ok(await _transactionService.ToListAsync(Guid.Parse(_userManager.GetUserId(User))));
    }

    // GET: api/Transactions/5
    /// <summary>
    /// Get an existing Transaction by ID
    /// </summary>
    /// <param name="id">Transaction ID (GUID)</param>
    /// <returns>Transaction that was retrieved by ID</returns>
    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Seller")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Transaction), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Transaction>> GetTransaction(Guid id)
    {
        var transaction = await _transactionService.FirstOrDefaultAsync(id, true, Guid.Parse(_userManager.GetUserId(User)));

        if (transaction == null)
        {
            return NotFound();
        }

        return transaction;
    }

    // PUT: api/Transactions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update an existing Transaction by ID
    /// </summary>
    /// <param name="id">Transaction ID (GUID)</param>
    /// <param name="transaction">Transaction details</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpPut("{id:guid}")]
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

        _transactionService.Update(transaction);
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
        transaction.Id = _transactionService.Add(transaction).Id;
        return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
    }

    // DELETE: api/Transactions/5
    /// <summary>
    /// Delete an existing Transaction by ID
    /// </summary>
    /// <param name="id">Transaction ID (GUID)</param>
    /// <returns>An empty Status204NoContent response</returns>
    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        var transaction = await _transactionService.FirstOrDefaultAsync(id, false, Guid.Parse(_userManager.GetUserId(User)));
        if (transaction == null)
        {
            return NotFound();
        }

        _transactionService.Remove(transaction);
        return NoContent();
    }
}