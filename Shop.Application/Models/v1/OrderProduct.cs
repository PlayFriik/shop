using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Models.v1;

public class OrderProduct
{
    public Guid Id { get; set; }
        
    public Guid OrderId { get; set; }
        
    public Guid ProductId { get; set; }
        
    public Product? Product { get; set; }
        
    [Range(1, int.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public int Quantity { get; set; }
}