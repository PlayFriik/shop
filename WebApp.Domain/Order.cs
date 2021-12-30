using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Domain.Interfaces;
using WebApp.Domain.Identity;

namespace WebApp.Domain;

public class Order : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
{
    public Guid AppUserId { get; set; }
        
    public AppUser? AppUser { get; set; }

    public Guid LocationId { get; set; }
        
    public Location? Location { get; set; }
        
    public Guid StatusId { get; set; }
        
    public Status? Status { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime DateTime { get; set; }

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public float Total { get; set; }
        
    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public string? Tracking { get; set; }

    public List<OrderProduct>? OrderProducts { get; set; }
        
    public List<Transaction>? Transactions { get; set; }
}