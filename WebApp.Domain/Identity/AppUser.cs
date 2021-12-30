using System.Collections.Generic;
using Base.Domain.Identity;

namespace WebApp.Domain.Identity;

public class AppUser : BaseUser<AppUserRole>
{
    public List<Order>? Orders { get; set; }
}