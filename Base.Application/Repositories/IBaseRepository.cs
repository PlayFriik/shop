namespace Base.Application.Repositories;

public interface IBaseRepository<TApplicationEntity>
    where TApplicationEntity : class
{
    Task<bool> AnyAsync(Guid id, Guid? userId = default);
    Task<IEnumerable<TApplicationEntity>> ToListAsync(Guid? userId = default, bool noTracking = true);
    Task<TApplicationEntity?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true);
    Task<TApplicationEntity?> RemoveAsync(Guid id, Guid? userId = default);

    TApplicationEntity Add(TApplicationEntity applicationEntity);
    TApplicationEntity Remove(TApplicationEntity applicationEntity, Guid? userId = default);
    TApplicationEntity Update(TApplicationEntity applicationEntity);
}