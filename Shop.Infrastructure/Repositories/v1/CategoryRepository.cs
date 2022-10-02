using AutoMapper;
using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;

namespace Shop.Infrastructure.Repositories.v1;

public class CategoryRepository : BaseRepository<Category, AppDbContext, Domain.Models.Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Category>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(category => category.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations);

        var domainEntities = await query.ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public override async Task<Category?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(category => category.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations);
        }

        var domainEntity = await query.FirstOrDefaultAsync(domainEntity => domainEntity.Id == id);
        return Mapper.Map(domainEntity);
    }

    public override Category Update(Category applicationEntity)
    {
        var domainEntity = Mapper.Map(applicationEntity);

        // Load the translations (will lose the DAL mapper translations)
        domainEntity.Name = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.NameId);

        return Mapper.Map(DbSet.Update(domainEntity).Entity);
    }
}