using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DAL.EF.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL.DTO.AutoMapper.Mappers;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.DAL.EF.Repositories;

public class ProductRepository : BaseRepository<AppDbContext, WebApp.DAL.DTO.Product, WebApp.Domain.Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProductMapper(mapper))
    {
            
    }
        
    public override async Task<IEnumerable<WebApp.DAL.DTO.Product>> ToListAsync(Guid userId = default, bool noTracking = true)
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
        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity));

        return dalEntities!;
    }
        
    public async Task<IEnumerable<WebApp.DAL.DTO.Product>> ToListSortedAsync(Guid? categoryId, string? sortBy, Guid userId = default, bool noTracking = true)
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
        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity));

        if (categoryId != null)
        {
            dalEntities = dalEntities.Where(product => product!.CategoryId == categoryId).ToList();
        }
            
        if (sortBy != null)
        {
            switch (sortBy.ToLower())
            {
                case "best-sellers":
                    dalEntities = dalEntities.OrderBy(product => product!.Sold).Reverse().ToList();
                    break;
                case "title-ascending":
                    dalEntities = dalEntities.OrderBy(product => product!.Name!.ToString()).ToList();
                    break;
                case "title-descending":
                    dalEntities = dalEntities.OrderBy(product => product!.Name!.ToString()).Reverse().ToList();
                    break;
                case "price-ascending":
                    dalEntities = dalEntities.OrderBy(product => product!.Price).ToList();
                    break;
                case "price-descending":
                    dalEntities = dalEntities.OrderBy(product => product!.Price).Reverse().ToList();
                    break;
            }
        }
            
        return dalEntities!;
    }

    public override async Task<WebApp.DAL.DTO.Product?> FirstOrDefaultAsync(Guid id, bool include, Guid userId = default, bool noTracking = true)
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
        var dalEntity = Mapper.Map(domainEntity);

        return dalEntity;
    }

    public override WebApp.DAL.DTO.Product Update(WebApp.DAL.DTO.Product entity)
    {
        var domainEntity = Mapper.Map(entity);

        // Load the translations (will lose the DAL mapper translations)
        domainEntity!.Name = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.NameId);
            
        domainEntity!.Description = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.DescriptionId);
            
        // Set the value from DAL entity back to list
        domainEntity!.Name.SetTranslation(entity.Name);
        domainEntity!.Description.SetTranslation(entity.Description);
            
        var updatedEntity = DbSet.Update(domainEntity!).Entity;
        var dalEntity = Mapper.Map(updatedEntity);
            
        return dalEntity!;
    }
}