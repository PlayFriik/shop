#pragma warning disable 1591
namespace WebApp.Models.Products;

public class Index
{
    public Guid? CategoryId { get; set; }
        
    public string? SortBy { get; set; }
        
    public List<WebApp.BLL.DTO.Category> Categories { get; set; } = default!;
        
    public List<WebApp.BLL.DTO.Product> Products { get; set; } = default!;
}