using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class OrdersController : Controller
{
    private readonly IAppBLL _bll;
    private readonly UserManager<WebApp.Domain.Identity.AppUser> _userManager;

    public OrdersController(IAppBLL bll, UserManager<WebApp.Domain.Identity.AppUser> userManager)
    {
        _bll = bll;
        _userManager = userManager;
    }

    // GET: Orders
    [Authorize]
    public async Task<IActionResult> Index()
    {
        return User.IsInRole("Admin") || User.IsInRole("Seller")
            ? View(await _bll.Orders.ToListAsync())
            : View(await _bll.Orders.ToListAsync(Guid.Parse(_userManager.GetUserId(User))));
    }

    // GET: Orders/Details/5
    [Authorize]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = User.IsInRole("Admin") || User.IsInRole("Seller")
            ? await _bll.Orders.FirstOrDefaultAsync(id.Value, true)
            : await _bll.Orders.FirstOrDefaultAsync(id.Value, true, Guid.Parse(_userManager.GetUserId(User)));
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // GET: Orders/Create
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var model = new Models.Orders.Create
        {
            AppUsers = new SelectList(_userManager.Users, "Id", "Email"),
            Locations = new SelectList(await _bll.Locations.ToListAsync(), "Id", "Name"),
            Statuses = new SelectList(await _bll.Statuses.ToListAsync(), "Id", "Name")
        };
            
        return View(model);
    }

    // POST: Orders/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Models.Orders.Create create)
    {
        if (ModelState.IsValid)
        {
            var order = new WebApp.BLL.DTO.Order
            {
                Id = Guid.NewGuid(),
                AppUserId = create.AppUserId,
                LocationId = create.LocationId,
                StatusId = create.StatusId,
                DateTime = create.DateTime,
                Total = create.Total,
                Tracking = create.Tracking
            };
                
            _bll.Orders.Add(order);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        var model = new Models.Orders.Create
        {
            AppUserId = create.AppUserId,
            LocationId = create.LocationId,
            StatusId = create.StatusId,
            DateTime = create.DateTime,
            Total = create.Total,
            Tracking = create.Tracking,
            AppUsers = new SelectList(_userManager.Users, "Id", "Email", create.AppUserId),
            Locations = new SelectList(await _bll.Locations.ToListAsync(), "Id", "Name", create.LocationId),
            Statuses = new SelectList(await _bll.Statuses.ToListAsync(), "Id", "Name", create.StatusId)
        };

        return View(model);
    }

    // GET: Orders/Edit/5
    [Authorize(Roles = "Admin,Seller")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _bll.Orders.FirstOrDefaultAsync(id.Value, true);
        if (order == null)
        {
            return NotFound();
        }

        var model = new Models.Orders.Edit
        {
            Id = order.Id,
            AppUserId = order.AppUserId,
            LocationId = order.LocationId,
            StatusId = order.StatusId,
            DateTime = order.DateTime,
            Total = order.Total,
            Tracking = order.Tracking,
            AppUsers = new SelectList(_userManager.Users, "Id", "Email", order.AppUserId),
            Locations = new SelectList(await _bll.Locations.ToListAsync(), "Id", "Name", order.LocationId),
            Statuses = new SelectList(await _bll.Statuses.ToListAsync(), "Id", "Name", order.StatusId)
        };
            
        return View(model);
    }

    // POST: Orders/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin,Seller")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Models.Orders.Edit edit)
    {
        if (id != edit.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var order = new WebApp.BLL.DTO.Order
                {
                    Id = edit.Id,
                    AppUserId = edit.AppUserId,
                    LocationId = edit.LocationId,
                    StatusId = edit.StatusId,
                    DateTime = edit.DateTime,
                    Total = edit.Total,
                    Tracking = edit.Tracking
                };
                    
                _bll.Orders.Update(order);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExists(edit.Id, Guid.Parse(_userManager.GetUserId(User))))
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

        var model = new Models.Orders.Edit
        {
            Id = edit.Id,
            AppUserId = edit.AppUserId,
            LocationId = edit.LocationId,
            StatusId = edit.StatusId,
            DateTime = edit.DateTime,
            Total = edit.Total,
            Tracking = edit.Tracking,
            AppUsers = new SelectList(_userManager.Users, "Id", "Email", edit.AppUserId),
            Locations = new SelectList(await _bll.Locations.ToListAsync(), "Id", "Name", edit.LocationId),
            Statuses = new SelectList(await _bll.Statuses.ToListAsync(), "Id", "Name", edit.StatusId)
        };

        return View(model);
    }

    // GET: Orders/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _bll.Orders.FirstOrDefaultAsync(id.Value, true);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // POST: Orders/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var order = await _bll.Orders.FirstOrDefaultAsync(id, false);
        if (order == null)
        {
            return RedirectToAction(nameof(Index));
        }

        _bll.Orders.Remove(order);
        await _bll.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> OrderExists(Guid id, Guid userId)
    {
        return await _bll.Orders.AnyAsync(id, userId);
    }
}