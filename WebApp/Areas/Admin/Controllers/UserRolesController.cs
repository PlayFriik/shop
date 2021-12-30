using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Areas.Admin.Models.UserRoles;
using WebApp.Domain.Identity;

#pragma warning disable 1591
namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserRolesController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public UserRolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET: Admin/UserRoles
    public async Task<IActionResult> Index()
    {
        var indexViewModel = new IndexViewModel
        {
            UserManager = _userManager,
            RoleManager = _roleManager,
            Users = await _userManager.Users.ToListAsync()
        };

        return View(indexViewModel);
    }

    // GET: Admin/UserRoles/Details/5
    public async Task<IActionResult> Details(Guid? userId, Guid? roleId)
    {
        if (userId == null || roleId == null)
        {
            return NotFound();
        }

        var appUser = await _userManager.Users.FirstOrDefaultAsync(appUser => appUser.Id == userId);
        var appRole = await _roleManager.Roles.FirstOrDefaultAsync(appRole => appRole.Id == roleId);
        if (appUser == null || appRole == null)
        {
            return NotFound();
        }
            
        var detailsViewModel = new DetailsViewModel
        {
            User = appUser,
            Role = appRole
        };

        return View(detailsViewModel);
    }

    // GET: Admin/UserRoles/Create
    public IActionResult Create()
    {
        var createViewModel = new CreateViewModel
        {
            Users = new SelectList(_userManager.Users, "Id", "UserName"),
            Roles = new SelectList(_roleManager.Roles, "Id", "Name"),
        };
            
        return View(createViewModel);
    }

    // POST: Admin/UserRoles/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateViewModel createViewModel)
    {
        if (ModelState.IsValid)
        {
            var appUser = _userManager.Users.FirstOrDefaultAsync(appUser => appUser.Id == createViewModel.User.Id).Result;
            var appRole = _roleManager.Roles.FirstOrDefaultAsync(appRole => appRole.Id == createViewModel.Role.Id).Result;
            if (appUser == null || appRole == null)
            {
                return View(createViewModel);
            }

            await _userManager.AddToRoleAsync(appUser, appRole.Name);

            return RedirectToAction(nameof(Index));
        }
            
        return View(createViewModel);
    }

    // GET: Admin/UserRoles/Delete/5
    public async Task<IActionResult> Delete(Guid? userId, Guid? roleId)
    {
        if (userId == null || roleId == null)
        {
            return NotFound();
        }

        var appUser = await _userManager.Users.FirstOrDefaultAsync(appUser => appUser.Id == userId);
        var appRole = await _roleManager.Roles.FirstOrDefaultAsync(appRole => appRole.Id == roleId);
        if (appUser == null || appRole == null)
        {
            return NotFound();
        }

        var deleteViewModel = new DeleteViewModel
        {
            User = appUser,
            Role = appRole
        };

        return View(deleteViewModel);
    }

    // POST: Admin/UserRoles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(DeleteViewModel deleteViewModel)
    {
        Debug.Print("USER ID: " + deleteViewModel.User.Id);
        Debug.Print("ROLE ID: " + deleteViewModel.Role.Id);
            
        var appUser = await _userManager.Users.FirstOrDefaultAsync(appUser => appUser.Id == deleteViewModel.User.Id);
        var appRole = await _roleManager.Roles.FirstOrDefaultAsync(appRole => appRole.Id == deleteViewModel.Role.Id);
        if (appUser == null || appRole == null)
        {
            return RedirectToAction(nameof(Index));
        }
            
        await _userManager.RemoveFromRoleAsync(appUser, appRole.Name);
            
        return RedirectToAction(nameof(Index));
    }
}