using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace WebApp.Domain;

public class Product : DomainEntityId
{
    public Guid CategoryId { get; set; }
        
    public Category? Category { get; set; }

    public Guid NameId { get; set; }
        
    [InverseProperty(nameof(AppTranslationString.ProductName))]
    public AppTranslationString? Name { get; set; }
        
    public Guid DescriptionId { get; set; }
        
    [InverseProperty(nameof(AppTranslationString.ProductDescription))]
    public AppTranslationString? Description { get; set; }

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public float Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public int Sold { get; set; }

    public List<Picture>? Pictures { get; set; }
}