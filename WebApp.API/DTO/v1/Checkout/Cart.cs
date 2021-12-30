using System;
using System.Collections.Generic;

namespace WebApp.API.DTO.v1.Checkout;

public class Cart
{
    public List<Entry> Entries { get; set; } = null!;
        
    public Guid LocationId { get; set; }
    public string LocationName { get; set; } = null!;
        
    public Guid ProviderId { get; set; }
    public string ProviderName { get; set; } = null!;
    public float ProviderPrice { get; set; }
        
    public Guid AppUserId { get; set; }
}