using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Base.DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApp.DAL.DTO.AutoMapper.Mappers;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.DAL.EF.Repositories;

public class CategoryRepository : BaseRepository<AppDbContext, WebApp.DAL.DTO.Category, WebApp.Domain.Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CategoryMapper(mapper))
    {
            
    }

    public override async Task<IEnumerable<WebApp.DAL.DTO.Category>> ToListAsync(Guid userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(category => category.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations);

        var domainEntities = await query.ToListAsync();
        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
            
        return dalEntities!;
    }
        
    public override async Task<WebApp.DAL.DTO.Category?> FirstOrDefaultAsync(Guid id, bool include, Guid userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(category => category.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations);
        }

        return Mapper.Map(await query.FirstOrDefaultAsync(category => category.Id == id));
    }

    public override WebApp.DAL.DTO.Category Update(WebApp.DAL.DTO.Category entity)
    {
        var domainEntity = Mapper.Map(entity);

        // Load the translations (will lose the DAL mapper translations)
        domainEntity!.Name = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.NameId);
            
        // Set the value from DAL entity back to list
        domainEntity!.Name.SetTranslation(entity.Name);
            
        var updatedEntity = DbSet.Update(domainEntity!).Entity;
        var dalEntity = Mapper.Map(updatedEntity);
            
        return dalEntity!;
    }
}