using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly IAppBLL _bll;

    public ProductsController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: Products
    [AllowAnonymous]
    public async Task<IActionResult> Index(Guid? category, string? sortBy)
    {
        var categories = (await _bll.Categories.ToListAsync()).ToList();
            
        List<WebApp.BLL.DTO.Product> products;
        if (category != null || sortBy != null)
        {
            products = (await _bll.Products.ToListSortedAsync(category, sortBy)).ToList();
        }
        else
        {
            products = (await _bll.Products.ToListAsync()).ToList();
        }

        var viewModel = new WebApp.Models.Products.Index
        {
            CategoryId = category,
            SortBy = sortBy,
            Categories = categories,
            Products = products
        };
            
        return View(viewModel);
    }

    // GET: Products/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _bll.Products.FirstOrDefaultAsync(id.Value, true);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: Products/Create
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var model = new Models.Products.Create
        {
            Categories = new SelectList(await _bll.Categories.ToListAsync(), "Id", "Name")
        };
            
        return View(model);
    }

    // POST: Products/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Models.Products.Create create)
    {
        if (ModelState.IsValid)
        {
            var product = new WebApp.BLL.DTO.Product
            {
                Id = Guid.NewGuid(),
                CategoryId = create.CategoryId,
                Name = create.Name,
                Description = create.Description,
                Price = create.Price,
                Quantity = create.Quantity,
                Sold = create.Sold
            };
                
            _bll.Products.Add(product);
            await _bll.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }
            
        var model = new Models.Products.Create
        {
            CategoryId = create.CategoryId,
            Name = create.Name,
            Description = create.Description,
            Price = create.Price,
            Quantity = create.Quantity,
            Sold = create.Sold,
            Categories = new SelectList(await _bll.Categories.ToListAsync(), "Id", "Name", create.CategoryId)
        };
            
        return View(model);
    }

    // GET: Products/Edit/5
    [Authorize(Roles = "Admin,Seller")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _bll.Products.FirstOrDefaultAsync(id.Value, true);
        if (product == null)
        {
            return NotFound();
        }

        var model = new Models.Products.Edit
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            NameId = product.NameId,
            Name = product.Name,
            DescriptionId = product.DescriptionId,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            Sold = product.Sold,
            Pictures = product.Pictures,
            Categories = new SelectList(await _bll.Categories.ToListAsync(), "Id", "Name", product.CategoryId)
        };
            
        return View(model);
    }

    // POST: Products/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin,Seller")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Models.Products.Edit edit)
    {
        if (id != edit.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var product = new WebApp.BLL.DTO.Product
                {
                    Id = edit.Id,
                    CategoryId = edit.CategoryId,
                    NameId = edit.NameId,
                    Name = edit.Name,
                    DescriptionId = edit.DescriptionId,
                    Description = edit.Description,
                    Price = edit.Price,
                    Quantity = edit.Quantity,
                    Sold = edit.Sold
                };
                    
                _bll.Products.Update(product);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(edit.Id))
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

        var model = new Models.Products.Edit
        {
            Id = edit.Id,
            CategoryId = edit.CategoryId,
            NameId = edit.NameId,
            Name = edit.Name,
            DescriptionId = edit.DescriptionId,
            Description = edit.Description,
            Price = edit.Price,
            Quantity = edit.Quantity,
            Sold = edit.Sold,
            Pictures = (await _bll.Products.FirstOrDefaultAsync(edit.Id, true))?.Pictures ?? null,
            Categories = new SelectList(await _bll.Categories.ToListAsync(), "Id", "Name", edit.CategoryId)
        };

        return View(model);
    }

    // GET: Products/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _bll.Products.FirstOrDefaultAsync(id.Value, true);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var product = await _bll.Products.FirstOrDefaultAsync(id, false);
        if (product == null)
        {
            return RedirectToAction(nameof(Index));
        }
            
        _bll.Products.Remove(product);
        await _bll.SaveChangesAsync();
            
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ProductExists(Guid id)
    {
        return await _bll.Products.AnyAsync(id);
    }
}