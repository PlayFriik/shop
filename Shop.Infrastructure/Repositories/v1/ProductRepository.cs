using AutoMapper;
using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;

namespace Shop.Infrastructure.Repositories.v1;

public class ProductRepository : BaseRepository<Product, AppDbContext, Domain.Models.Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Product>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(product => product.Category)
            .ThenInclude(category => category!.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations)
            .Include(product => product.Pictures)
            .Include(product => product.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations)
            .Include(product => product.Description)
            .ThenInclude(descriptionTranslationString => descriptionTranslationString!.Translations)
            .AsSplitQuery();

        var domainEntities = await query.ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public override async Task<Product?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(product => product.Category)
                .ThenInclude(category => category!.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations)
                .Include(product => product.Pictures)
                .Include(product => product.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations)
                .Include(product => product.Description)
                .ThenInclude(descriptionTranslationString => descriptionTranslationString!.Translations)
                .AsSplitQuery();
        }

        var domainEntity = await query.FirstOrDefaultAsync(domainEntity => domainEntity.Id == id);
        return Mapper.Map(domainEntity);
    }

    public override Product Update(Product applicationEntity)
    {
        var domainEntity = Mapper.Map(applicationEntity);

        // Load the translations (will lose the DAL mapper translations)
        domainEntity.Name = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.NameId);

        domainEntity.Description = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.DescriptionId);

        return Mapper.Map(DbSet.Update(domainEntity).Entity);
    }
}