using AutoMapper;
using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;

namespace Shop.Infrastructure.Repositories.v1;

public class ProviderRepository : BaseRepository<Provider, AppDbContext, Domain.Models.Provider>, IProviderRepository
{
    public ProviderRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Provider>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(provider => provider.Name)
            .ThenInclude(translationString => translationString!.Translations);

        var domainEntities = await query.ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public override async Task<Provider?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(provider => provider.Name)
                .ThenInclude(translationString => translationString!.Translations);
        }

        var domainEntity = await query.FirstOrDefaultAsync(domainEntity => domainEntity.Id == id);
        return Mapper.Map(domainEntity);
    }

    public override Provider Update(Provider applicationEntity)
    {
        var domainEntity = Mapper.Map(applicationEntity);

        // Load the translations (will lose the DAL mapper translations)
        domainEntity.Name = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.NameId);

        return Mapper.Map(DbSet.Update(domainEntity).Entity);
    }
}