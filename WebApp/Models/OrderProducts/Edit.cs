﻿using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable 1591
namespace WebApp.Models.OrderProducts;

public class Edit
{
    public Guid Id { get; set; }
        
    public Guid OrderId { get; set; }
        
    public Guid ProductId { get; set; }
        
    public int Quantity { get; set; }

    public SelectList? Orders { get; set; }
        
    public SelectList? Products { get; set; }
}