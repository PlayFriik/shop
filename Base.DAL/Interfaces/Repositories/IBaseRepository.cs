using System;
using Base.Domain.Interfaces;

namespace Base.DAL.Interfaces.Repositories;

public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
    where TEntity : class, IDomainEntityId<Guid> // any more rules? maybe ID?
{
        
}

public interface IBaseRepository<TEntity, TKey> : IBaseRepositoryAsync<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey> // any more rules? maybe ID?
    where TKey : IEquatable<TKey>
{
        
}