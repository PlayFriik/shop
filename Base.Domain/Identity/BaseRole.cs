using System;
using System.Collections.Generic;
using Base.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Identity;

public class BaseRole<TUserRole> : BaseRole<Guid, TUserRole>, IDomainEntityId
    where TUserRole : IdentityUserRole<Guid>
{
        
}

public class BaseRole<TKey, TUserRole> : IdentityRole<TKey>, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TUserRole : IdentityUserRole<TKey>
{
    public virtual List<TUserRole>? UserRoles { get; set; }
}