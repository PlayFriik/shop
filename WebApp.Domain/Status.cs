using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace WebApp.Domain;

public class Status : DomainEntityId
{
    public Guid NameId { get; set; }
        
    [InverseProperty(nameof(AppTranslationString.StatusName))]
    public AppTranslationString? Name { get; set; }

    public List<Order>? Orders { get; set; }
}