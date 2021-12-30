using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Base.DAL.Interfaces.DTO.AutoMapper.Mappers;
using Base.DAL.Interfaces.Repositories;
using Base.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF.Repositories;

public class BaseRepository<TDbContext, TDALEntity, TDomainEntity> : BaseRepository<TDbContext, TDALEntity, TDomainEntity, Guid>, IBaseRepository<TDALEntity>
    where TDbContext : DbContext
    where TDALEntity : class, IDomainEntityId
    where TDomainEntity : class, IDomainEntityId
{
    public BaseRepository(TDbContext dbContext, IBaseMapper<TDALEntity, TDomainEntity> mapper) : base(dbContext, mapper)
    {
            
    }
}

public class BaseRepository<TDbContext, TDALEntity, TDomainEntity, TKey> : IBaseRepository<TDALEntity, TKey>
    where TDbContext : DbContext
    where TDALEntity : class, IDomainEntityId<TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly TDbContext DbContext;
    protected readonly DbSet<TDomainEntity> DbSet;
    protected readonly IBaseMapper<TDALEntity, TDomainEntity> Mapper;

    protected BaseRepository(TDbContext dbContext, IBaseMapper<TDALEntity, TDomainEntity> mapper)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }
        
    protected IQueryable<TDomainEntity> GetQuery(TKey? userId = default, bool noTracking = true)
    {
        var query = DbSet.AsQueryable();

        // TODO: validate the input entity also
        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            query = query.Where(e => ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public virtual TDALEntity Add(TDALEntity entity)
    {
        return Mapper.Map(DbSet.Add(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDALEntity Remove(TDALEntity entity, TKey? userId = default)
    {
        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)) &&
            !((IDomainAppUserId<TKey>) entity).AppUserId.Equals(userId))
        {
            throw new AuthenticationException($"Bad user id inside entity {typeof(TDALEntity).Name} to be deleted.");
            // TODO: load entity from the db, check that userId inside entity is correct.
        }

        return Mapper.Map(DbSet.Remove(Mapper.Map(entity)!).Entity)!;        
    }
        
    public virtual TDALEntity Update(TDALEntity entity)
    {
        var domainEntity = Mapper.Map(entity);
        var updatedEntity = DbSet.Update(domainEntity!).Entity;
        var dalEntity = Mapper.Map(updatedEntity);
            
        return dalEntity!;
    }

    public virtual async Task<bool> AnyAsync(TKey id, TKey? userId = default)
    {
        if (userId == null || userId.Equals(default))
        {
            // No ownership control, userId is null or default (null or 0)
            return await DbSet.AnyAsync(e => e.Id.Equals(id));
        }

        // We have userId and it is not null or default (null or 0) - so we should check for AppUserId too
        // Does the entity actually implement the correct interface:
        if (!typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
        {
            throw new AuthenticationException("Entity does not implement required interface: {typeof(IDomainAppUserId<TKey>).FullName)} for AppUserId check");
        }

        // ReSharper disable once SuspiciousTypeConversion.Global
        return await DbSet.AnyAsync(domainEntity => domainEntity.Id.Equals(id) && ((IDomainAppUserId<TKey>) domainEntity).AppUserId.Equals(userId));
    }
        
    public virtual async Task<IEnumerable<TDALEntity>> ToListAsync(TKey? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        var select = query.Select(domainEntity => Mapper.Map(domainEntity));
        var list = await select.ToListAsync();
            
        return list!;
    }

    public virtual async Task<TDALEntity?> FirstOrDefaultAsync(TKey id, bool include, TKey? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
            
        return Mapper.Map(await query.FirstOrDefaultAsync(dalEntity => dalEntity!.Id.Equals(id)));
    }
        
    public virtual async Task<TDALEntity?> RemoveAsync(TKey id, TKey? userId = default)
    {
        var entity = await FirstOrDefaultAsync(id, false, userId);
        if (entity == null)
        {
            return null;
        }
            
        return Remove(entity);
    }
}