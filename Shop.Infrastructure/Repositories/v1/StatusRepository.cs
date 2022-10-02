using AutoMapper;
using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;

namespace Shop.Infrastructure.Repositories.v1;

public class StatusRepository : BaseRepository<Status, AppDbContext, Domain.Models.Status>, IStatusRepository
{
    public StatusRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Status>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(status => status.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations);

        var domainEntities = await query.ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public override async Task<Status?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(status => status.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations);
        }

        var domainEntity = await query.FirstOrDefaultAsync(domainEntity => domainEntity.Id == id);
        return Mapper.Map(domainEntity);
    }

    public override Status Update(Status applicationEntity)
    {
        var domainEntity = Mapper.Map(applicationEntity);

        // Load the translations (will lose the DAL mapper translations)
        domainEntity.Name = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.NameId);

        return Mapper.Map(DbSet.Update(domainEntity).Entity);
    }
}