using System;
using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Base.Resources;

namespace WebApp.BLL.DTO;

public class Transaction : DomainEntityId
{
    public Guid OrderId { get; set; }
        
    public Order? Order { get; set; }

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Common))]
    public float Amount { get; set; }
}