using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Domain.Interfaces;

namespace Base.DAL.Interfaces.Repositories;

public interface IBaseRepositoryAsync<TEntity, TKey> : IBaseRepositoryCommon<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey> // any more rules? maybe ID?
    where TKey : IEquatable<TKey>
{
    Task<bool> AnyAsync(TKey id, TKey? userId = default);
    Task<IEnumerable<TEntity>> ToListAsync(TKey? userId = default, bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(TKey id, bool include, TKey? userId = default, bool noTracking = true);
    Task<TEntity?> RemoveAsync(TKey id, TKey? userId = default);
}