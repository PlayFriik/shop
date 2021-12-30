using System;

namespace Base.Domain.Interfaces;

public interface IDomainAppUserId : IDomainAppUserId<Guid>
{
        
}
    
public interface IDomainAppUserId<TKey>
    where TKey : IEquatable<TKey>
{
    TKey AppUserId { get; set; }
}