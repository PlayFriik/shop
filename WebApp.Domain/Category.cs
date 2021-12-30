using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace WebApp.Domain;

public class Category : DomainEntityId
{
    public Guid NameId { get; set; }
        
    [InverseProperty(nameof(AppTranslationString.CategoryName))]
    public AppTranslationString? Name { get; set; }

    public List<Product>? Products { get; set; }
}