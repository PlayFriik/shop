using System;
using Base.Domain.Interfaces;

namespace Base.DAL.Interfaces.Repositories;

public interface IBaseRepositoryCommon<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey> // any more rules? maybe ID?
    where TKey : IEquatable<TKey>
{
    TEntity Add(TEntity entity);
    TEntity Remove(TEntity entity, TKey? userId = default);
    TEntity Update(TEntity entity);
}