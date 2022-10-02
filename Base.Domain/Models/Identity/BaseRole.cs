using System;
using System.Collections.Generic;
using Base.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Models.Identity;

public class BaseRole<TUserRole> : IdentityRole<Guid>, IDomainEntityId
    where TUserRole : IdentityUserRole<Guid>
{
    public virtual List<TUserRole>? UserRoles { get; set; }
}