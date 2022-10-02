using Base.Application.Repositories;
using Base.Application.Services;

namespace Base.Infrastructure.Services;

public class BaseService<TApplicationEntity, TRepository> : IBaseService<TApplicationEntity>
    where TApplicationEntity : class
    where TRepository : IBaseRepository<TApplicationEntity>
{
    protected readonly TRepository Repository;

    public BaseService(TRepository serviceRepository)
    {
        Repository = serviceRepository;
    }
        
    public virtual TApplicationEntity Add(TApplicationEntity applicationEntity)
    {
        return Repository.Add(applicationEntity);
    }

    public virtual TApplicationEntity Remove(TApplicationEntity applicationEntity, Guid? userId = default)
    {
        return Repository.Remove(applicationEntity, userId);
    }
        
    public virtual TApplicationEntity Update(TApplicationEntity applicationEntity)
    {
        return Repository.Update(applicationEntity);
    }

    public virtual async Task<bool> AnyAsync(Guid id, Guid? userId = default)
    {
        return await Repository.AnyAsync(id, userId);
    }
        
    public virtual async Task<IEnumerable<TApplicationEntity>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        return await Repository.ToListAsync(userId, noTracking);
    }

    public virtual async Task<TApplicationEntity?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        return await Repository.FirstOrDefaultAsync(id, include, userId, noTracking);
    }
        
    public virtual async Task<TApplicationEntity?> RemoveAsync(Guid id, Guid? userId = default)
    {
        return await Repository.RemoveAsync(id, userId);
    }
}