using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class PicturesController : Controller
{
    private readonly IAppBLL _bll;

    public PicturesController(IAppBLL bll)
    {
        _bll = bll;
    }
        
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        return View(await _bll.Pictures.ToListAsync());
    }

    // GET: Pictures/Create
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Guid? productId)
    {
        if (productId == null)
        {
            return NotFound();
        }
            
        var product = await _bll.Products.FirstOrDefaultAsync(productId.Value, true);
        if (product == null)
        {
            return NotFound();
        }

        var model = new Models.Pictures.Create
        {
            ProductId = productId.Value
        };
            
        return View(model);
    }

    // POST: Pictures/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Models.Pictures.Create create)
    {
        if (ModelState.IsValid)
        {
            var picture = new WebApp.BLL.DTO.Picture
            {
                ProductId = create.ProductId,
                Path = create.Path
            };
                
            _bll.Pictures.Add(picture);
            await _bll.SaveChangesAsync();
                
            return RedirectToAction("Edit", "Products", new { id = picture.ProductId });
        }
            
        return View(create);
    }

    // GET: Pictures/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var picture = await _bll.Pictures.FirstOrDefaultAsync(id.Value, true);
        if (picture == null)
        {
            return NotFound();
        }

        return View(picture);
    }

    // POST: Pictures/Delete/5
    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var picture = await _bll.Pictures.FirstOrDefaultAsync(id, false);
        if (picture == null)
        {
            return RedirectToAction("Delete", "Pictures", new { id });
        }

        _bll.Pictures.Remove(picture);
        await _bll.SaveChangesAsync();
            
        return RedirectToAction("Edit", "Products", new { id = picture.ProductId });
    }
}