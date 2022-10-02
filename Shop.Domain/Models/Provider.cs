using Base.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Models;

public class Provider : DomainEntityId
{
    public Guid NameId { get; set; }
        
    [InverseProperty(nameof(AppTranslationString.ProviderName))]
    public AppTranslationString? Name { get; set; }

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public float Price { get; set; }

    public List<Location>? Locations { get; set; }
}