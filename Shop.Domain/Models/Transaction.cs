using Base.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Models;

public class Transaction : DomainEntityId
{
    public Guid OrderId { get; set; }
        
    public Order? Order { get; set; }

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public float Amount { get; set; }
}