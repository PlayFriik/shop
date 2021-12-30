using System;

namespace WebApp.BLL.DTO.Checkout;

public class Entry
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public float ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
        
    public int Quantity { get; set; }
        
    public float Total { get; set; }
}