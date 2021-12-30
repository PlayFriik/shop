using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.DAL.DTO;

public class Location : DomainEntityId
{
    public Guid ProviderId { get; set; }
        
    public Provider? Provider { get; set; }

    public Guid NameId { get; set; }
        
    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Name { get; set; } = null!;

    public Guid AddressId { get; set; }
        
    [MaxLength(1024, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string Address { get; set; } = null!;
        
    public List<Order>? Orders { get; set; }
}