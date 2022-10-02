using Base.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Models;

public class Status : DomainEntityId
{
    public Guid NameId { get; set; }
        
    [InverseProperty(nameof(AppTranslationString.StatusName))]
    public AppTranslationString? Name { get; set; }

    public List<Order>? Orders { get; set; }
}