using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DAL.Interfaces;

namespace Base.DAL;

public abstract class BaseUnitOfWork : IBaseUnitOfWork
{
    public abstract Task<int> SaveChangesAsync();
        
    private readonly Dictionary<Type, object> _cache = new();
    public TRepository GetRepository<TRepository>(Func<TRepository> instantiate)
        where TRepository : class
    {
        if (_cache.TryGetValue(typeof(TRepository), out var result))
        {
            return (TRepository) result;
        }

        var repository = instantiate();
        _cache.Add(typeof(TRepository), repository);
            
        return repository;
    }
}