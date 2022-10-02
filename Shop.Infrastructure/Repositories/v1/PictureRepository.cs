using AutoMapper;
using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;

namespace Shop.Infrastructure.Repositories.v1;

public class PictureRepository : BaseRepository<Picture, AppDbContext, Domain.Models.Picture>, IPictureRepository
{
    public PictureRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Picture>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(picture => picture.Product)
            .ThenInclude(product => product!.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations);

        var domainEntities = await query.ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public override async Task<Picture?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(picture => picture.Product)
                .ThenInclude(product => product!.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations);
        }

        var domainEntity = await query.FirstOrDefaultAsync(domainEntity => domainEntity.Id == id);
        return Mapper.Map(domainEntity);
    }
}