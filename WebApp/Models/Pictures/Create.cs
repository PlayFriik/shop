using System.ComponentModel.DataAnnotations;

#pragma warning disable 1591
namespace WebApp.Models.Pictures;

public class Create
{
    public Guid ProductId { get; set; }
        
    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Path { get; set; } = null!;
}