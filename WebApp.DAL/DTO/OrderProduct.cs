using System;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.DAL.DTO;

public class OrderProduct : DomainEntityId
{
    public Guid OrderId { get; set; }
        
    public Order? Order { get; set; }
        
    public Guid ProductId { get; set; }
        
    public Product? Product { get; set; }

    [Range(1, int.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Resources.Common))]
    public int Quantity { get; set; }
}