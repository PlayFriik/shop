using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.BLL.Interfaces.DTO.AutoMapper.Mappers;
using Base.BLL.Interfaces.Services;
using Base.DAL.Interfaces;
using Base.DAL.Interfaces.Repositories;
using Base.Domain.Interfaces;

namespace Base.BLL.Services;

public class BaseService<TUnitOfWork, TRepository, TBLLEntity, TDALEntity> : BaseService<TUnitOfWork, TRepository, TBLLEntity, TDALEntity, Guid>, IBaseService<TBLLEntity, TDALEntity>
    where TBLLEntity : class, IDomainEntityId
    where TDALEntity : class, IDomainEntityId
    where TUnitOfWork : IBaseUnitOfWork
    where TRepository : IBaseRepository<TDALEntity>
{
    public BaseService(TUnitOfWork serviceUow, TRepository serviceRepository, IBaseMapper<TBLLEntity, TDALEntity> mapper) : base(serviceUow, serviceRepository, mapper)
    {
            
    }
}

public class BaseService<TUnitOfWork, TRepository, TBLLEntity, TDALEntity, TKey> : IBaseService<TBLLEntity, TDALEntity, TKey>
    where TBLLEntity : class, IDomainEntityId<TKey>
    where TDALEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TUnitOfWork : IBaseUnitOfWork
    where TRepository : IBaseRepository<TDALEntity, TKey>
{
    protected readonly TUnitOfWork ServiceUow;
    protected readonly TRepository ServiceRepository;
    protected readonly IBaseMapper<TBLLEntity, TDALEntity> Mapper;

    public BaseService(TUnitOfWork serviceUow, TRepository serviceRepository, IBaseMapper<TBLLEntity, TDALEntity> mapper)
    {
        ServiceUow = serviceUow;
        ServiceRepository = serviceRepository;
        Mapper = mapper;
    }
        
    public virtual TBLLEntity Add(TBLLEntity entity)
    {
        return Mapper.Map(ServiceRepository.Add(Mapper.Map(entity)!))!;
    }

    public virtual TBLLEntity Remove(TBLLEntity entity, TKey? userId = default)
    {
        return Mapper.Map(ServiceRepository.Remove(Mapper.Map(entity)!, userId))!;
    }
        
    public virtual TBLLEntity Update(TBLLEntity entity)
    {
        return Mapper.Map(ServiceRepository.Update(Mapper.Map(entity)!))!;
    }

    public virtual async Task<bool> AnyAsync(TKey id, TKey? userId = default)
    {
        return await ServiceRepository.AnyAsync(id, userId);
    }
        
    public virtual async Task<IEnumerable<TBLLEntity>> ToListAsync(TKey? userId = default, bool noTracking = true)
    {
        return (await ServiceRepository.ToListAsync(userId, noTracking)).Select(entity => Mapper.Map(entity))!;
    }

    public virtual async Task<TBLLEntity?> FirstOrDefaultAsync(TKey id, bool include, TKey? userId = default, bool noTracking = true)
    {
        return Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, include, userId, noTracking));
    }
        
    public virtual async Task<TBLLEntity?> RemoveAsync(TKey id, TKey? userId = default)
    {
        return Mapper.Map(await ServiceRepository.RemoveAsync(id, userId))!;
    }
}