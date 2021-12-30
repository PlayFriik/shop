using System.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using WebApp.BLL.Interfaces;
using WebApp.Models;

#pragma warning disable 1591
namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly IAppBLL _bll;

    public HomeController(IAppBLL bll)
    {
        _bll = bll;
    }

    public async Task<IActionResult> Index()
    {
        var products = (await _bll.Products.ToListAsync()).ToList();
        var nowAvailable = products.LastOrDefault();
        var bestSellers = products.OrderByDescending(product => product.Sold).ToList();
            
        var model = new WebApp.Models.Home.Index
        {
            NowAvailable = nowAvailable,
            BestSellers = bestSellers
        };
            
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
        
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(
                new RequestCulture(culture)
            ),
            new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1)
            }
        );

        return LocalRedirect(returnUrl);
    }
}