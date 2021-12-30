using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable 1591
namespace WebApp.Models.Orders;

public class Edit
{
    public Guid Id { get; set; }
        
    public Guid AppUserId { get; set; }
        
    public Guid LocationId { get; set; }
        
    public Guid StatusId { get; set; }
        
    public DateTime DateTime { get; set; }

    public float Total { get; set; }
        
    public string? Tracking { get; set; }
        
    public SelectList? AppUsers { get; set; }
        
    public SelectList? Locations { get; set; }

    public SelectList? Statuses { get; set; }
}