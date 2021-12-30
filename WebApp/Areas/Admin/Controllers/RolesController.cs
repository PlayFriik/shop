using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Domain.Identity;

#pragma warning disable 1591
namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<AppRole> _roleManager;

    public RolesController(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    // GET: Admin/Roles
    public async Task<IActionResult> Index()
    {
        return View(await _roleManager.Roles.ToListAsync());
    }

    // GET: Admin/Roles/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appRole = await _roleManager.Roles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (appRole == null)
        {
            return NotFound();
        }

        return View(appRole);
    }

    // GET: Admin/Roles/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Roles/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AppRole appRole)
    {
        if (ModelState.IsValid)
        {
            await _roleManager.CreateAsync(appRole);
                
            return RedirectToAction(nameof(Index));
        }
        return View(appRole);
    }

    // GET: Admin/Roles/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appRole = await _roleManager.Roles.FirstOrDefaultAsync(appRole => appRole.Id == id);
        if (appRole == null)
        {
            return NotFound();
        }
            
        return View(appRole);
    }

    // POST: Admin/Roles/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, AppRole appRole)
    {
        if (id != appRole.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _roleManager.UpdateAsync(appRole);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppRoleExists(appRole.Id))
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
            
        return View(appRole);
    }

    // GET: Admin/Roles/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appRole = await _roleManager.Roles.FirstOrDefaultAsync(appRole => appRole.Id == id);
        if (appRole == null)
        {
            return NotFound();
        }

        return View(appRole);
    }

    // POST: Admin/Roles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var appRole = await _roleManager.Roles.FirstOrDefaultAsync(appRole => appRole.Id == id);
        await _roleManager.DeleteAsync(appRole);
            
        return RedirectToAction(nameof(Index));
    }

    private bool AppRoleExists(Guid id)
    {
        return _roleManager.Roles.Any(appRole => appRole.Id == id);
    }
}