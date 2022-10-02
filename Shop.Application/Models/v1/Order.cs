using Shop.Application.Models.v1.Identity;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Models.v1;

public class Order
{
    public Guid Id { get; set; }
        
    public Guid AppUserId { get; set; }
        
    public AppUser? AppUser { get; set; }

    public Guid LocationId { get; set; }
        
    public Location? Location { get; set; }
        
    public Guid StatusId { get; set; }
        
    public Status? Status { get; set; }
        
    [DataType(DataType.DateTime)]
    public DateTime DateTime { get; set; }

    [Range(0.00, float.MaxValue, ErrorMessageResourceName = "ErrorMessage_Range", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public float Total { get; set; }
        
    [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Base.Domain.Resources.Common))]
    public string? Tracking { get; set; }
        
    public List<OrderProduct>? OrderProducts { get; set; }
        
    public List<Transaction>? Transactions { get; set; }
}