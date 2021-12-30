using System;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Interfaces;

public interface IDomainAppUser<TAppUser> : IDomainAppUser<TAppUser, Guid>
    where TAppUser : IdentityUser<Guid>
{
        
}

public interface IDomainAppUser<TAppUser, TKey>
    where TAppUser : IdentityUser<TKey>
    where TKey : IEquatable<TKey>
{
    TAppUser? AppUser { get; set; }
}