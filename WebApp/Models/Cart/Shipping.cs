using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable 1591
namespace WebApp.Models.Cart;

public class Shipping
{
    public Guid ProviderId { get; set; }
        
    public Guid LocationId { get; set; }
        
    public SelectList? Providers { get; set; }
}