using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class CartController : Controller
{
    private readonly IAppBLL _bll;

    public CartController(IAppBLL bll)
    {
        _bll = bll;
    }

    // GET: Cart
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var cart = new WebApp.BLL.DTO.Checkout.Cart();
        if (Request.Cookies["Cart"] != null)
        {
            var deserializedCart = JsonSerializer.Deserialize<WebApp.BLL.DTO.Checkout.Cart>(Request.Cookies["Cart"]!);

            if (deserializedCart != null)
            {
                foreach (var deserializedCartEntry in deserializedCart.Entries)
                {
                    var product = await _bll.Products.FirstOrDefaultAsync(deserializedCartEntry.ProductId, true);
                    if (product == null)
                    {
                        continue;
                    }
                        
                    deserializedCartEntry.ProductId = product.Id;
                    deserializedCartEntry.ProductName = product.Name;
                    deserializedCartEntry.ProductPrice = product.Price;
                    deserializedCartEntry.ProductQuantity = product.Quantity;

                    if (product.Quantity <= 0)
                    {
                        continue;
                    }

                    if (deserializedCartEntry.Quantity > deserializedCartEntry.ProductQuantity)
                    {
                        deserializedCartEntry.Quantity = deserializedCartEntry.ProductQuantity;
                    }
                        
                    cart.Entries.Add(deserializedCartEntry);
                }
            }
        }
            
        if (cart.Entries.Count >= 1)
        {
            Response.Cookies.Append(
                "Cart",
                JsonSerializer.Serialize(cart),
                new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                }
            );
        }

        return View();
    }

    // GET: Cart/Shipping
    [AllowAnonymous]
    public async Task<IActionResult> Shipping()
    {
        var model = new Models.Cart.Shipping
        {
            Providers = new SelectList(await _bll.Providers.ToListAsync(), "Id", "Name")
        };

        return View(model);
    }

    // POST: Cart/AddEntry
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> AddEntry([FromForm] WebApp.BLL.DTO.Checkout.Entry add)
    {
        var cart = new WebApp.BLL.DTO.Checkout.Cart();
        if (Request.Cookies["Cart"] != null)
        {
            var deserializedCart = JsonSerializer.Deserialize<WebApp.BLL.DTO.Checkout.Cart>(Request.Cookies["Cart"]!);

            if (deserializedCart != null)
            {
                foreach (var deserializedCartEntry in deserializedCart.Entries)
                {
                    var product = await _bll.Products.FirstOrDefaultAsync(deserializedCartEntry.ProductId, true);
                    if (product == null)
                    {
                        continue;
                    }

                    deserializedCartEntry.ProductId = product.Id;
                    deserializedCartEntry.ProductName = product.Name;
                    deserializedCartEntry.ProductPrice = product.Price;
                    deserializedCartEntry.ProductQuantity = product.Quantity;

                    if (product.Quantity <= 0)
                    {
                        continue;
                    }

                    if (deserializedCartEntry.Quantity > deserializedCartEntry.ProductQuantity)
                    {
                        deserializedCartEntry.Quantity = deserializedCartEntry.ProductQuantity;
                    }
                        
                    cart.Entries.Add(deserializedCartEntry);
                }
            }
        }

        if (ModelState.IsValid)
        {
            if (add.ProductQuantity <= 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            var entry = cart.Entries.FirstOrDefault(entry => entry.ProductId == add.ProductId);
            if (entry == null)
            {
                if (add.Quantity < 1)
                {
                    add.Quantity = 1;
                }
                else if (add.Quantity > add.ProductQuantity)
                {
                    add.Quantity = add.ProductQuantity;
                }
                add.Total = add.Quantity * add.ProductPrice;

                cart.Entries.Add(add);
            }
            else
            {
                if (entry.Quantity + add.Quantity > add.ProductQuantity) {
                    entry.Quantity = add.ProductQuantity;
                }
                else
                {
                    entry.Quantity += add.Quantity;
                }
                entry.Total = entry.Quantity * entry.ProductPrice;
            }

            if (cart.Entries.Count >= 1)
            {
                Response.Cookies.Append(
                    "Cart",
                    JsonSerializer.Serialize(cart),
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    }
                );
            }

            return RedirectToAction("Index", "Cart");
        }

        return RedirectToAction("Index", "Cart");
    }

    // POST: Cart/ChangeEntry
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ChangeEntry([FromForm] WebApp.BLL.DTO.Checkout.Entry change)
    {
        var cart = new WebApp.BLL.DTO.Checkout.Cart();
        if (Request.Cookies["Cart"] != null)
        {
            var deserializedCart = JsonSerializer.Deserialize<WebApp.BLL.DTO.Checkout.Cart>(Request.Cookies["Cart"]!);

            if (deserializedCart != null)
            {
                foreach (var deserializedCartEntry in deserializedCart.Entries)
                {
                    var product = await _bll.Products.FirstOrDefaultAsync(deserializedCartEntry.ProductId, true);
                    if (product == null)
                    {
                        continue;
                    }

                    deserializedCartEntry.ProductId = product.Id;
                    deserializedCartEntry.ProductName = product.Name;
                    deserializedCartEntry.ProductPrice = product.Price;
                    deserializedCartEntry.ProductQuantity = product.Quantity;

                    if (product.Quantity <= 0)
                    {
                        continue;
                    }

                    if (deserializedCartEntry.Quantity > deserializedCartEntry.ProductQuantity)
                    {
                        deserializedCartEntry.Quantity = deserializedCartEntry.ProductQuantity;
                    }
                        
                    cart.Entries.Add(deserializedCartEntry);
                }
            }
        }

        var entry = cart.Entries.FirstOrDefault(entry => entry.ProductId == change.ProductId);
        if (entry == null)
        {
            return RedirectToAction("Index", "Cart");
        }
            
        if (change.Quantity == -1 && entry.Quantity > 1) {
            entry.Quantity -= 1;
            entry.Total = change.Total;
        } else if (change.Quantity == 1 && entry.Quantity < entry.ProductQuantity) {
            entry.Quantity += 1;
            entry.Total = change.Total;
        }

        if (cart.Entries.Count >= 1)
        {
            Response.Cookies.Append(
                "Cart",
                JsonSerializer.Serialize(cart),
                new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                }
            );
        }

        return RedirectToAction("Index", "Cart");
    }

    // POST: Cart/RemoveEntry
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RemoveEntry([FromForm] Guid productId)
    {
        var cart = new WebApp.BLL.DTO.Checkout.Cart();
        if (Request.Cookies["Cart"] != null)
        {
            var deserializedCart = JsonSerializer.Deserialize<WebApp.BLL.DTO.Checkout.Cart>(Request.Cookies["Cart"]!);

            if (deserializedCart != null)
            {
                foreach (var deserializedCartEntry in deserializedCart.Entries)
                {
                    var product = await _bll.Products.FirstOrDefaultAsync(deserializedCartEntry.ProductId, true);
                    if (product == null)
                    {
                        continue;
                    }

                    deserializedCartEntry.ProductId = product.Id;
                    deserializedCartEntry.ProductName = product.Name;
                    deserializedCartEntry.ProductPrice = product.Price;
                    deserializedCartEntry.ProductQuantity = product.Quantity;

                    if (product.Quantity <= 0)
                    {
                        continue;
                    }

                    if (deserializedCartEntry.Quantity > deserializedCartEntry.ProductQuantity)
                    {
                        deserializedCartEntry.Quantity = deserializedCartEntry.ProductQuantity;
                    }

                    cart.Entries.Add(deserializedCartEntry);
                }
            }
        }

        var entry = cart.Entries.FirstOrDefault(entry => entry.ProductId == productId);
        if (entry == null)
        {
            return RedirectToAction("Index", "Cart");
        }

        cart.Entries.Remove(entry);

        if (cart.Entries.Count >= 1)
        {
            Response.Cookies.Append(
                "Cart",
                JsonSerializer.Serialize(cart),
                new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                }
            );
        }
        else
        {
            Response.Cookies.Delete("Cart");
        }

        return RedirectToAction("Index", "Cart");
    }
        
    // POST: Cart/SetShipping
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SetShipping([FromForm] Models.Cart.Shipping model)
    {
        if (model.ProviderId == Guid.Empty ||
            model.LocationId == Guid.Empty) {
            return RedirectToAction("Shipping", "Cart");
        }
            
        var cart = new WebApp.BLL.DTO.Checkout.Cart();
        if (Request.Cookies["Cart"] != null)
        {
            var deserializedCart = JsonSerializer.Deserialize<WebApp.BLL.DTO.Checkout.Cart>(Request.Cookies["Cart"]!);

            if (deserializedCart != null)
            {
                foreach (var deserializedCartEntry in deserializedCart.Entries)
                {
                    var product = await _bll.Products.FirstOrDefaultAsync(deserializedCartEntry.ProductId, true);
                    if (product == null)
                    {
                        continue;
                    }

                    deserializedCartEntry.ProductId = product.Id;
                    deserializedCartEntry.ProductName = product.Name;
                    deserializedCartEntry.ProductPrice = product.Price;
                    deserializedCartEntry.ProductQuantity = product.Quantity;

                    if (product.Quantity <= 0)
                    {
                        continue;
                    }

                    if (deserializedCartEntry.Quantity > deserializedCartEntry.ProductQuantity)
                    {
                        deserializedCartEntry.Quantity = deserializedCartEntry.ProductQuantity;
                    }
                        
                    cart.Entries.Add(deserializedCartEntry);
                }
            }
        }

        var location = await _bll.Locations.FirstOrDefaultAsync(model.LocationId, true);
        if (location == null)
        {
            return RedirectToAction("Shipping", "Cart");
        }

        cart.LocationId = location.Id;
        cart.LocationName = location.Name;
            
        var provider = await _bll.Providers.FirstOrDefaultAsync(model.ProviderId, true);
        if (provider == null)
        {
            return RedirectToAction("Shipping", "Cart");
        }
            
        cart.ProviderId = provider.Id;
        cart.ProviderName = provider.Name;
        cart.ProviderPrice = provider.Price;
            
        Response.Cookies.Append(
            "Cart",
            JsonSerializer.Serialize(cart),
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            }
        );

        return RedirectToAction("Index", "Checkout");
    }
}