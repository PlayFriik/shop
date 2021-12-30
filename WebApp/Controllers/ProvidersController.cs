using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class ProvidersController : Controller
{
    private readonly IAppBLL _bll;

    public ProvidersController(IAppBLL bll)
    {
        _bll = bll;
    }
        
    // GET: Providers
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await _bll.Providers.ToListAsync());
    }

    // GET: Providers/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var provider = await _bll.Providers.FirstOrDefaultAsync(id.Value, true);
        if (provider == null)
        {
            return NotFound();
        }

        return View(provider);
    }

    // GET: Providers/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Providers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WebApp.BLL.DTO.Provider provider)
    {
        if (ModelState.IsValid)
        {
            _bll.Providers.Add(provider);
            await _bll.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }
            
        return View(provider);
    }

    // GET: Providers/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var provider = await _bll.Providers.FirstOrDefaultAsync(id.Value, true);
        if (provider == null)
        {
            return NotFound();
        }
            
        return View(provider);
    }

    // POST: Providers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, WebApp.BLL.DTO.Provider provider)
    {
        if (id != provider.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _bll.Providers.Update(provider);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProviderExists(provider.Id))
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
            
        return View(provider);
    }

    // GET: Providers/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var provider = await _bll.Providers.FirstOrDefaultAsync(id.Value, true);
        if (provider == null)
        {
            return NotFound();
        }

        return View(provider);
    }

    // POST: Providers/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var provider = await _bll.Providers.FirstOrDefaultAsync(id, false);
        if (provider == null)
        {
            return RedirectToAction(nameof(Index));
        }
            
        _bll.Providers.Remove(provider);
        await _bll.SaveChangesAsync();
            
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ProviderExists(Guid id)
    {
        return await _bll.Providers.AnyAsync(id);
    }
}