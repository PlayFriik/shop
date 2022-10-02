using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Models.v1;

public class Product
{
    public Guid Id { get; set; }
        
    public Guid CategoryId { get; set; }
        
    public Category? Category { get; set; }

    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string Name { get; set; } = null!;

    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string Description { get; set; } = null!;

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public float Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public int Sold { get; set; }
        
    public List<Picture>? Pictures { get; set; }
}