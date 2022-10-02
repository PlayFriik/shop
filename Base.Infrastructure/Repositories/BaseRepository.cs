using AutoMapper;
using Base.Application.Repositories;
using Base.Domain.Interfaces;
using Base.Infrastructure.Mappers;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Repositories;

public class BaseRepository<TApplicationEntity, TDbContext, TDomainEntity> : IBaseRepository<TApplicationEntity>
    where TApplicationEntity : class
    where TDbContext : DbContext
    where TDomainEntity : class, IDomainEntityId
{
    protected readonly TDbContext DbContext;
    protected readonly DbSet<TDomainEntity> DbSet;
    protected readonly BaseMapper<TDomainEntity, TApplicationEntity> Mapper;

    protected BaseRepository(TDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TDomainEntity>();
        Mapper = new BaseMapper<TDomainEntity, TApplicationEntity>(mapper);
    }

    protected IQueryable<TDomainEntity> GetQuery(Guid? userId = default, bool noTracking = true)
    {
        var query = DbSet.AsQueryable();

        // TODO: validate the input entity also
        if (userId != null && !userId.Equals(default) && typeof(IDomainAppUserId).IsAssignableFrom(typeof(TDomainEntity)))
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            query = query.Where(e => ((IDomainAppUserId)e).AppUserId.Equals(userId));
        }

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public virtual TApplicationEntity Add(TApplicationEntity applicationEntity)
    {
        var domainEntity = Mapper.Map(applicationEntity);
        domainEntity = DbSet.Add(domainEntity).Entity;

        DbContext.SaveChanges();
        return Mapper.Map(domainEntity);
    }

    public virtual TApplicationEntity Remove(TApplicationEntity applicationEntity, Guid? userId = default)
    {
        var domainEntity = Mapper.Map(applicationEntity);

        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainAppUserId).IsAssignableFrom(typeof(TDomainEntity)) &&
            !((IDomainAppUserId)domainEntity).AppUserId.Equals(userId))
        {
            throw new AuthenticationException($"Bad user id inside entity {typeof(TDomainEntity).Name} to be deleted.");
            // TODO: load entity from the db, check that userId inside entity is correct.
        }

        return Mapper.Map(DbSet.Remove(domainEntity).Entity);
    }

    public virtual TApplicationEntity Update(TApplicationEntity applicationEntity)
    {
        var domainEntity = Mapper.Map(applicationEntity);
        return Mapper.Map(DbSet.Update(domainEntity).Entity);
    }

    public virtual async Task<bool> AnyAsync(Guid id, Guid? userId = default)
    {
        if (userId == null || userId.Equals(default))
        {
            // No ownership control, userId is null or default (null or 0)
            return await DbSet.AnyAsync(e => e.Id.Equals(id));
        }

        // We have userId and it is not null or default (null or 0) - so we should check for AppUserId too
        // Does the entity actually implement the correct interface:
        if (!typeof(IDomainAppUserId).IsAssignableFrom(typeof(TDomainEntity)))
        {
            throw new AuthenticationException("Entity does not implement required interface: {typeof(IDomainAppUserId<TKey>).FullName)} for AppUserId check");
        }

        // ReSharper disable once SuspiciousTypeConversion.Global
        return await DbSet.AnyAsync(domainEntity => domainEntity.Id.Equals(id) && ((IDomainAppUserId)domainEntity).AppUserId.Equals(userId));
    }

    public virtual async Task<IEnumerable<TApplicationEntity>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var domainEntities = await GetQuery(userId, noTracking).ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public virtual async Task<TApplicationEntity?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var domainEntity = await GetQuery(userId, noTracking).FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        return Mapper.Map(domainEntity);
    }

    public virtual async Task<TApplicationEntity?> RemoveAsync(Guid id, Guid? userId = default)
    {
        var applicationEntity = await FirstOrDefaultAsync(id, false, userId);
        return applicationEntity == null ? null : Remove(applicationEntity);
    }
}