using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class LocationsController : Controller
{
    private readonly IAppBLL _bll;

    public LocationsController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: Locations
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await _bll.Locations.ToListAsync());
    }

    // GET: Locations/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var location = await _bll.Locations.FirstOrDefaultAsync(id.Value, true);
        if (location == null)
        {
            return NotFound();
        }

        return View(location);
    }

    // GET: Locations/Create
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var model = new Models.Locations.Create
        {
            Providers = new SelectList(await _bll.Providers.ToListAsync(), "Id", "Name")
        };
            
        return View(model);
    }

    // POST: Locations/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Models.Locations.Create create)
    {
        if (ModelState.IsValid)
        {
            var location = new WebApp.BLL.DTO.Location
            {
                Id = Guid.NewGuid(),
                ProviderId = create.ProviderId,
                Name = create.Name,
                Address = create.Address
            };
                
            _bll.Locations.Add(location);
            await _bll.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }
            
        var model = new Models.Locations.Create
        {
            Providers = new SelectList(await _bll.Providers.ToListAsync(), "Id", "Name", create.ProviderId)
        };

        return View(model);
    }

    // GET: Locations/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var location = await _bll.Locations.FirstOrDefaultAsync(id.Value, true);
        if (location == null)
        {
            return NotFound();
        }
            
        var model = new Models.Locations.Edit
        {
            Id = location.Id,
            ProviderId = location.ProviderId,
            NameId = location.NameId,
            Name = location.Name,
            AddressId = location.AddressId,
            Address = location.Address,
            Providers = new SelectList(await _bll.Providers.ToListAsync(), "Id", "Name", location.ProviderId)
        };

        return View(model);
    }

    // POST: Locations/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, WebApp.Models.Locations.Edit edit)
    {
        if (id != edit.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var location = new WebApp.BLL.DTO.Location
                {
                    Id = edit.Id,
                    ProviderId = edit.ProviderId,
                    NameId = edit.NameId,
                    Name = edit.Name,
                    AddressId = edit.AddressId,
                    Address = edit.Address
                };

                _bll.Locations.Update(location);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LocationExists(edit.Id))
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

        var model = new Models.Locations.Edit
        {
            Id = edit.Id,
            ProviderId = edit.ProviderId,
            NameId = edit.NameId,
            Name = edit.Name,
            AddressId = edit.AddressId,
            Address = edit.Address,
            Providers = new SelectList(await _bll.Providers.ToListAsync(), "Id", "Name", edit.ProviderId)
        };
            
        return View(model);
    }

    // GET: Locations/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var location = await _bll.Locations.FirstOrDefaultAsync(id.Value, true);
        if (location == null)
        {
            return NotFound();
        }

        return View(location);
    }

    // POST: Locations/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var location = await _bll.Locations.FirstOrDefaultAsync(id, false);
        if (location == null)
        {
            return RedirectToAction(nameof(Index));
        }
            
        _bll.Locations.Remove(location);
        await _bll.SaveChangesAsync();
            
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> LocationExists(Guid id)
    {
        return await _bll.Locations.AnyAsync(id);
    }
}