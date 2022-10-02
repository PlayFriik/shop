using Base.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Models;

public class Picture : DomainEntityId
{
    public Guid ProductId { get; set; }
        
    public Product? Product { get; set; }

    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string Path { get; set; } = null!;
}