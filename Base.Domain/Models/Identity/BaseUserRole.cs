using System;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Models.Identity;

public class BaseUserRole<TUser, TRole> : IdentityUserRole<Guid>
    where TUser: IdentityUser<Guid>
    where TRole: IdentityRole<Guid>
{
    public virtual TUser? User { get; set; }
    public virtual TRole? Role { get; set; }
}