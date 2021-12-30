using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp.DAL.EF;
using WebApp.Domain.Identity;

#pragma warning disable 1591
namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Users
    public async Task<IActionResult> Index()
    {
        return View(await _context.Users.ToListAsync());
    }

    // GET: Admin/Users/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appUser = await _context.Users.FirstOrDefaultAsync(appUser => appUser.Id == id);
        if (appUser == null)
        {
            return NotFound();
        }

        return View(appUser);
    }

    // GET: Admin/Users/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appUser = await _context.Users.FirstOrDefaultAsync(appUser => appUser.Id == id);
        if (appUser == null)
        {
            return NotFound();
        }

        return View(appUser);
    }

    // POST: Admin/Users/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, AppUser appUser)
    {
        if (id != appUser.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (appUser.LockoutEnabled)
                {
                    appUser.LockoutEnd = DateTime.MaxValue;
                }
                else
                {
                    appUser.LockoutEnd = null;
                }
                    
                _context.Users.Update(appUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(appUser.Id))
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
        else
        {
            foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
            {
                Debug.Print(modelError.ErrorMessage);
            }
        }

        return View(appUser);
    }

    private bool AppUserExists(Guid id)
    {
        return _context.Users.Any(appUser => appUser.Id == id);
    }
}