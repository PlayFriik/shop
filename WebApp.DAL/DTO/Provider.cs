using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.DAL.DTO;

public class Provider : DomainEntityId
{
    public Guid NameId { get; set; }
        
    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Name { get; set; } = null!;

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public float Price { get; set; }

    public List<Location>? Locations { get; set; }
}