using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable 1591
namespace WebApp.Models.Products;

public class Edit
{
    public Guid Id { get; set; }
        
    public Guid CategoryId { get; set; }
        
    public Guid NameId { get; set; }
        
    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Name { get; set; } = null!;
        
    public Guid DescriptionId { get; set; }
        
    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Description { get; set; } = null!;

    public float Price { get; set; }

    public int Quantity { get; set; }

    public int Sold { get; set; }
        
    public List<WebApp.BLL.DTO.Picture>? Pictures { get; set; }
        
    public SelectList? Categories { get; set; }
}