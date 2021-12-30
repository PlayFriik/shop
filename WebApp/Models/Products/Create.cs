using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable 1591
namespace WebApp.Models.Products;

public class Create
{
    public Guid CategoryId { get; set; }
        
    public string Name { get; set; } = null!;
        
    public string Description { get; set; } = null!;
        
    public float Price { get; set; }
        
    public int Quantity { get; set; }
        
    public int Sold { get; set; }
        
    public SelectList? Categories { get; set; }
}