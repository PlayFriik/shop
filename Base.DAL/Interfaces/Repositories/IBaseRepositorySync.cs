using System;
using System.Collections.Generic;
using Base.Domain.Interfaces;

namespace Base.DAL.Interfaces.Repositories;

public interface IBaseRepositorySync<TEntity, TKey> : IBaseRepositoryCommon<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey> // any more rules? maybe ID?
    where TKey : IEquatable<TKey>
{
    bool Any(TKey id, TKey? userId = default);
    IEnumerable<TEntity> ToList(TKey? userId = default, bool noTracking = true);
    TEntity Remove(TKey id, TKey? userId = default);
    TEntity? FirstOrDefault(TKey id, TKey? userId = default, bool noTracking = true);
}