using System;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.API.DTO.v1;

public class Transaction
{
    public Guid Id { get; set; }
        
    public Guid OrderId { get; set; }
        
    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public float Amount { get; set; }
}