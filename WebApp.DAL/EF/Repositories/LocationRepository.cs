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

public class LocationRepository : BaseRepository<AppDbContext, WebApp.DAL.DTO.Location, WebApp.Domain.Location>, ILocationRepository
{
    public LocationRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LocationMapper(mapper))
    {
            
    }

    public override async Task<IEnumerable<WebApp.DAL.DTO.Location>> ToListAsync(Guid userId = default, bool noTracking = true)
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
        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity));

        return dalEntities!;
    }

    public override async Task<WebApp.DAL.DTO.Location?> FirstOrDefaultAsync(Guid id, bool include, Guid userId = default, bool noTracking = true)
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

        return Mapper.Map(await query.FirstOrDefaultAsync(dalEntity => dalEntity.Id == id));
    }
        
    public override WebApp.DAL.DTO.Location Update(WebApp.DAL.DTO.Location entity)
    {
        var domainEntity = Mapper.Map(entity);

        // Load the translations (will lose the DAL mapper translations)
        domainEntity!.Name = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.NameId);
            
        domainEntity!.Address = DbContext.TranslationStrings
            .Include(t => t.Translations)
            .First(transactionString => transactionString.Id == domainEntity.AddressId);
            
        // Set the value from DAL entity back to list
        domainEntity!.Name.SetTranslation(entity.Name);
        domainEntity!.Address.SetTranslation(entity.Address);
            
        var updatedEntity = DbSet.Update(domainEntity!).Entity;
        var dalEntity = Mapper.Map(updatedEntity);
            
        return dalEntity!;
    }
}