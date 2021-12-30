using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.BLL.Interfaces;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class CheckoutController : Controller
{
    private readonly IAppBLL _bll;
    private readonly UserManager<WebApp.Domain.Identity.AppUser> _userManager;

    public CheckoutController(IAppBLL bll, UserManager<WebApp.Domain.Identity.AppUser> userManager)
    {
        _bll = bll;
        _userManager = userManager;
    }

    // GET: Checkout
    [Authorize]
    public IActionResult Index()
    {
        if (Request.Cookies["Cart"] == null)
        {
            return RedirectToAction("Index", "Cart");
        }
            
        var deserializedCart = JsonSerializer.Deserialize<WebApp.BLL.DTO.Checkout.Cart>(Request.Cookies["Cart"]!);
        if (deserializedCart == null || deserializedCart.Entries.Count <= 0)
        {
            return RedirectToAction("Index", "Cart");
        }
            
        Response.Cookies.Append(
            "Cart",
            JsonSerializer.Serialize(deserializedCart),
            new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            }
        );

        return View();
    }
        
    // GET: Checkout/Success
    [Authorize]
    public IActionResult Success(Guid orderId)
    {
        Response.Cookies.Delete("Cart");
            
        var model = new Models.Checkout.Success
        {
            OrderId = orderId
        };

        return View(model);
    }

    // GET: Checkout/Failure
    [Authorize]
    public IActionResult Failure()
    {
        return View();
    }
        
    // POST: Checkout/Process
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Process([FromBody] WebApp.BLL.DTO.Checkout.Cart cart)
    {
        cart.AppUserId = Guid.Parse(_userManager.GetUserId(User));

        var order = await _bll.Orders.Process(cart);
        return Ok(order);
    }
}