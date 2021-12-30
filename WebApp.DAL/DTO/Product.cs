using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.DAL.DTO;

public class Product : DomainEntityId
{
    public Guid CategoryId { get; set; }
        
    public Category? Category { get; set; }

    public Guid NameId { get; set; }
        
    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Name { get; set; } = null!;
        
    public Guid DescriptionId { get; set; }
        
    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Description { get; set; } = null!;

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public float Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public int Sold { get; set; }

    public List<Picture>? Pictures { get; set; }
}