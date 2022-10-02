using System;
using Base.Domain.Interfaces;

namespace Base.Domain.Models;

public abstract class DomainEntityId : DomainEntityId<Guid>, IDomainEntityId
{
}

public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey> 
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}