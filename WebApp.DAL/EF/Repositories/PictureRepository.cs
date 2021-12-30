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

public class PictureRepository : BaseRepository<AppDbContext, WebApp.DAL.DTO.Picture, WebApp.Domain.Picture>, IPictureRepository
{
    public PictureRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PictureMapper(mapper))
    {
            
    }
        
    public override async Task<IEnumerable<WebApp.DAL.DTO.Picture>> ToListAsync(Guid userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(picture => picture.Product)
            .ThenInclude(product => product!.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations);

        var domainEntities = await query.ToListAsync();
        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity));

        return dalEntities!;
    }

    public override async Task<WebApp.DAL.DTO.Picture?> FirstOrDefaultAsync(Guid id, bool include, Guid userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(picture => picture.Product)
                .ThenInclude(product => product!.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations);
        }

        return Mapper.Map(await query.FirstOrDefaultAsync(dalEntity => dalEntity.Id == id));
    }
}