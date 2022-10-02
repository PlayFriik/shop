using AutoMapper;
using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;

namespace Shop.Infrastructure.Repositories.v1;

public class LocationRepository : BaseRepository<Location, AppDbContext, Domain.Models.Location>, ILocationRepository
{
    public LocationRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Location>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(location => location.Provider)
            .ThenInclude(provider => provider!.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations)
            .Include(location => location.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations)
            .Include(location => location.Address)
            .ThenInclude(addressTranslationString => addressTranslationString!.Translations)
            .AsSplitQuery();

        var domainEntities = await query.ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public override async Task<Location?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(location => location.Provider)
                .ThenInclude(provider => provider!.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations)
                .Include(location => location.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations)
                .Include(location => location.Address)
                .ThenInclude(addressTranslationString => addressTranslationString!.Translations)
                .AsSplitQuery();
        }

        var domainEntity = await query.FirstOrDefaultAsync(domainEntity => domainEntity.Id == id);
        return Mapper.Map(domainEntity);
    }

    public override Location Update(Location applicationEntity)
    {
        var domainEntity = Mapper.Map(applicationEntity);

        // Load the translations (will lose the DAL mapper translations)
        domainEntity.Name = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.NameId);

        domainEntity.Address = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.AddressId);

        return Mapper.Map(DbSet.Update(domainEntity).Entity);
    }
}