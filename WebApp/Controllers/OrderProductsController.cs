using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class OrderProductsController : Controller
{
    private readonly IAppBLL _bll;

    public OrderProductsController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: OrderProducts
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        return View(await _bll.OrderProducts.ToListAsync());
    }

    // GET: OrderProducts/Details/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderProduct = await _bll.OrderProducts.FirstOrDefaultAsync(id.Value, true);
        if (orderProduct == null)
        {
            return NotFound();
        }

        return View(orderProduct);
    }

    // GET: OrderProducts/Create
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var model = new Models.OrderProducts.Create
        {
            Orders = new SelectList(await _bll.Orders.ToListAsync(), "Id", "Id"),
            Products = new SelectList(await _bll.Products.ToListAsync(), "Id", "Name")
        };
            
        return View(model);
    }

    // POST: OrderProducts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Models.OrderProducts.Create create)
    {
        if (ModelState.IsValid)
        {
            var orderProduct = new WebApp.BLL.DTO.OrderProduct
            {
                Id = Guid.NewGuid(),
                OrderId = create.OrderId,
                ProductId = create.ProductId,
                Quantity = create.Quantity
            };
                
            _bll.OrderProducts.Add(orderProduct);
            await _bll.SaveChangesAsync();
                
            return RedirectToAction(nameof(Index));
        }
            
        var model = new Models.OrderProducts.Create
        {
            Orders = new SelectList(await _bll.Orders.ToListAsync(), "Id", "Id", create.OrderId),
            Products = new SelectList(await _bll.Products.ToListAsync(), "Id", "Name", create.ProductId)
        };
            
        return View(model);
    }

    // GET: OrderProducts/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderProduct = await _bll.OrderProducts.FirstOrDefaultAsync(id.Value, true);
        if (orderProduct == null)
        {
            return NotFound();
        }
            
        var model = new Models.OrderProducts.Edit
        {
            Id = orderProduct.Id,
            OrderId = orderProduct.OrderId,
            ProductId = orderProduct.ProductId,
            Quantity = orderProduct.Quantity,
            Orders = new SelectList(await _bll.Orders.ToListAsync(), "Id", "Id", orderProduct.OrderId),
            Products = new SelectList(await _bll.Products.ToListAsync(), "Id", "Name", orderProduct.ProductId)
        };

        return View(model);
    }

    // POST: OrderProducts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, Models.OrderProducts.Edit edit)
    {
        if (id != edit.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var orderProduct = new WebApp.BLL.DTO.OrderProduct
                {
                    Id = edit.Id,
                    OrderId = edit.OrderId,
                    ProductId = edit.ProductId,
                    Quantity = edit.Quantity
                };
                    
                _bll.OrderProducts.Update(orderProduct);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderProductExists(edit.Id))
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
            
        var model = new Models.OrderProducts.Edit
        {
            Id = edit.Id,
            OrderId = edit.OrderId,
            ProductId = edit.ProductId,
            Quantity = edit.Quantity,
            Orders = new SelectList(await _bll.Orders.ToListAsync(), "Id", "Id", edit.OrderId),
            Products = new SelectList(await _bll.Products.ToListAsync(), "Id", "Name", edit.ProductId)
        };
            
        return View(model);
    }

    // GET: OrderProducts/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderProduct = await _bll.OrderProducts.FirstOrDefaultAsync(id.Value, true);
        if (orderProduct == null)
        {
            return NotFound();
        }

        return View(orderProduct);
    }

    // POST: OrderProducts/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var orderProduct = await _bll.OrderProducts.FirstOrDefaultAsync(id, false);
        if (orderProduct == null)
        {
            return RedirectToAction(nameof(Index));
        }
            
        _bll.OrderProducts.Remove(orderProduct);
        await _bll.SaveChangesAsync();
            
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> OrderProductExists(Guid id)
    {
        return await _bll.OrderProducts.AnyAsync(id);
    }
}