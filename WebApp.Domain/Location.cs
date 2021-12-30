using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace WebApp.Domain;

public class Location : DomainEntityId
{
    public Guid ProviderId { get; set; }
        
    public Provider? Provider { get; set; }

    public Guid NameId { get; set; }
        
    [InverseProperty(nameof(AppTranslationString.LocationName))]
    public AppTranslationString? Name { get; set; }

    public Guid AddressId { get; set; }
        
    [InverseProperty(nameof(AppTranslationString.LocationAddress))]
    public AppTranslationString? Address { get; set; }
        
    public List<Order>? Orders { get; set; }
}