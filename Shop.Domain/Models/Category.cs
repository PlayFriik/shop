using Base.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Models;

public class Category : DomainEntityId
{
    public Guid NameId { get; set; }
        
    [InverseProperty(nameof(AppTranslationString.CategoryName))]
    public AppTranslationString? Name { get; set; }

    public List<Product>? Products { get; set; }
}