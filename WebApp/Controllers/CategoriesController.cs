using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class CategoriesController : Controller
{
    private readonly IAppBLL _bll;

    public CategoriesController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: Categories
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await _bll.Categories.ToListAsync());
    }

    // GET: Categories/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _bll.Categories.FirstOrDefaultAsync(id.Value, true);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: Categories/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WebApp.BLL.DTO.Category category)
    {
        if (ModelState.IsValid)
        {
            _bll.Categories.Add(category);
            await _bll.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }
            
        return View(category);
    }

    // GET: Categories/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _bll.Categories.FirstOrDefaultAsync(id.Value, true);
        if (category == null)
        {
            return NotFound();
        }
            
        return View(category);
    }

    // POST: Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, WebApp.BLL.DTO.Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _bll.Categories.Update(category);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(category.Id))
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
            
        return View(category);
    }

    // GET: Categories/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _bll.Categories.FirstOrDefaultAsync(id.Value, true);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var category = await _bll.Categories.FirstOrDefaultAsync(id, false);
        if (category == null)
        {
            return RedirectToAction(nameof(Index));
        }
            
        _bll.Categories.Remove(category);
        await _bll.SaveChangesAsync();
            
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CategoryExists(Guid id)
    {
        return await _bll.Categories.AnyAsync(id);
    }
}