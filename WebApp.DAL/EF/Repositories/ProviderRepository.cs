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

public class ProviderRepository : BaseRepository<AppDbContext, WebApp.DAL.DTO.Provider, WebApp.Domain.Provider>, IProviderRepository
{
    public ProviderRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProviderMapper(mapper))
    {
            
    }
        
    public override async Task<IEnumerable<WebApp.DAL.DTO.Provider>> ToListAsync(Guid userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(provider => provider.Name)
            .ThenInclude(translationString => translationString!.Translations);
            
        var domainEntities = await query.ToListAsync();
        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity));

        return dalEntities!;
    }
        
    public override async Task<WebApp.DAL.DTO.Provider?> FirstOrDefaultAsync(Guid id, bool include, Guid userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(provider => provider.Name)
                .ThenInclude(translationString => translationString!.Translations);
        }

        return Mapper.Map(await query.FirstOrDefaultAsync(provider => provider.Id == id));
    }
        
    public override WebApp.DAL.DTO.Provider Update(WebApp.DAL.DTO.Provider entity)
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