using AutoMapper;
using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;

namespace Shop.Infrastructure.Repositories.v1;

public class OrderProductRepository : BaseRepository<OrderProduct, AppDbContext, Domain.Models.OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<OrderProduct>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(orderProduct => orderProduct.Order)
            .Include(orderProduct => orderProduct.Product)
            .ThenInclude(product => product!.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations);

        var domainEntities = await query.ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public override async Task<OrderProduct?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(orderProduct => orderProduct.Order)
                .Include(orderProduct => orderProduct.Product)
                .ThenInclude(product => product!.Name)
                .ThenInclude(nameTranslationString => nameTranslationString!.Translations);
        }

        var domainEntity = await query.FirstOrDefaultAsync(domainEntity => domainEntity.Id == id);
        return Mapper.Map(domainEntity);
    }
}