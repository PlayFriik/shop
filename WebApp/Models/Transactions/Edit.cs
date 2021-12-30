using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable 1591
namespace WebApp.Models.Transactions;

public class Edit
{
    public Guid Id { get; set; }
        
    public Guid OrderId { get; set; }

    public float Amount { get; set; }
        
    public SelectList? Orders { get; set; }
}