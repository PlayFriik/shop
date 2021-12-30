using System;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.DAL.DTO;

public class Transaction : DomainEntityId
{
    public Guid OrderId { get; set; }
        
    public Order? Order { get; set; }

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public float Amount { get; set; }
}