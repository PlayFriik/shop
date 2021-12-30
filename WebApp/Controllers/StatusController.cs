using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class StatusesController : Controller
{
    private readonly IAppBLL _bll;

    public StatusesController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: Statuses
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        return View(await _bll.Statuses.ToListAsync());
    }

    // GET: Statuses/Details/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var status = await _bll.Statuses.FirstOrDefaultAsync(id.Value, true);
        if (status == null)
        {
            return NotFound();
        }

        return View(status);
    }

    // GET: Statuses/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Statuses/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WebApp.BLL.DTO.Status status)
    {
        if (ModelState.IsValid)
        {
            _bll.Statuses.Add(status);
            await _bll.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }
            
        return View(status);
    }

    // GET: Statuses/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var status = await _bll.Statuses.FirstOrDefaultAsync(id.Value, true);
        if (status == null)
        {
            return NotFound();
        }
            
        return View(status);
    }

    // POST: Statuses/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, WebApp.BLL.DTO.Status status)
    {
        if (id != status.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _bll.Statuses.Update(status);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StatusExists(status.Id))
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
            
        return View(status);
    }

    // GET: Statuses/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var status = await _bll.Statuses.FirstOrDefaultAsync(id.Value, true);
        if (status == null)
        {
            return NotFound();
        }

        return View(status);
    }

    // POST: Statuses/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var status = await _bll.Statuses.FirstOrDefaultAsync(id, false);
        if (status == null)
        {
            return RedirectToAction(nameof(Index));
        }
            
        _bll.Statuses.Remove(status);
        await _bll.SaveChangesAsync();
            
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> StatusExists(Guid id)
    {
        return await _bll.Statuses.AnyAsync(id);
    }
}