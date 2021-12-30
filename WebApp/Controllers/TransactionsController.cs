using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class TransactionsController : Controller
{
    private readonly IAppBLL _bll;
    private readonly UserManager<WebApp.Domain.Identity.AppUser> _userManager;

    public TransactionsController(IAppBLL bll, UserManager<WebApp.Domain.Identity.AppUser> userManager)
    {
        _bll = bll;
        _userManager = userManager;
    }

    // GET: Transactions
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        return View(await _bll.Transactions.ToListAsync());
    }

    // GET: Transactions/Details/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _bll.Transactions.FirstOrDefaultAsync(id.Value, true);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // GET: Transactions/Create
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var model = new Models.Transactions.Create
        {
            Orders = new SelectList(await _bll.Orders.ToListAsync(), "Id", "Id")
        };
            
        return View(model);
    }

    // POST: Transactions/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Models.Transactions.Create create)
    {
        if (ModelState.IsValid)
        {
            var transaction = new WebApp.BLL.DTO.Transaction
            {
                Id = Guid.NewGuid(),
                OrderId = create.OrderId,
                Amount = create.Amount
            };
                
            _bll.Transactions.Add(transaction);
            await _bll.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }
            
        var model = new Models.Transactions.Create
        {
            OrderId = create.OrderId,
            Amount = create.Amount,
            Orders = new SelectList(await _bll.Orders.ToListAsync(), "Id", "Id", create.OrderId)
        };
            
        return View(model);
    }

    // GET: Transactions/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _bll.Transactions.FirstOrDefaultAsync(id.Value, true);
        if (transaction == null)
        {
            return NotFound();
        }

        var model = new Models.Transactions.Edit
        {
            Id = transaction.Id,
            OrderId = transaction.OrderId,
            Amount = transaction.Amount,
            Orders = new SelectList(await _bll.Orders.ToListAsync(), "Id", "Id", transaction.OrderId)
        };
            
        return View(model);
    }

    // POST: Transactions/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Models.Transactions.Edit edit)
    {
        if (id != edit.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var transaction = new WebApp.BLL.DTO.Transaction
                {
                    Id = edit.Id,
                    OrderId = edit.OrderId,
                    Amount = edit.Amount
                };
                    
                _bll.Transactions.Update(transaction);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TransactionExists(edit.Id, Guid.Parse(_userManager.GetUserId(User))))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
                
            return RedirectToAction(nameof(Index));
        }
            
        var model = new Models.Transactions.Edit
        {
            Id = edit.Id,
            OrderId = edit.OrderId,
            Amount = edit.Amount,
            Orders = new SelectList(await _bll.Orders.ToListAsync(), "Id", "Id", edit.OrderId)
        };
            
        return View(model);
    }

    // GET: Transactions/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transaction = await _bll.Transactions.FirstOrDefaultAsync(id.Value, true);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // POST: Transactions/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var transaction = await _bll.Transactions.FirstOrDefaultAsync(id, false);
        if (transaction == null)
        {
            return RedirectToAction(nameof(Index));
        }
            
        _bll.Transactions.Remove(transaction);
        await _bll.SaveChangesAsync();
            
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> TransactionExists(Guid id, Guid userId)
    {
        return await _bll.Transactions.AnyAsync(id, userId);
    }
}