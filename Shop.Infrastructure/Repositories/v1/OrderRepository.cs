using AutoMapper;
using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;

namespace Shop.Infrastructure.Repositories.v1;

public class OrderRepository : BaseRepository<Order, AppDbContext, Domain.Models.Order>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Order>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(order => order.AppUser)
            .Include(order => order.Location)
            .ThenInclude(location => location!.Name)
            .ThenInclude(locationTranslationString => locationTranslationString!.Translations)
            .Include(order => order.Status)
            .ThenInclude(status => status!.Name)
            .ThenInclude(statusTranslationString => statusTranslationString!.Translations)
            .OrderByDescending(order => order.DateTime)
            .AsSplitQuery();

        var domainEntities = await query.ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public override async Task<Order?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(order => order.AppUser)
                .Include(order => order.Location)
                .ThenInclude(location => location!.Name)
                .ThenInclude(locationTranslationString => locationTranslationString!.Translations)
                .Include(order => order.Location!.Provider)
                .ThenInclude(provider => provider!.Name)
                .ThenInclude(providerTranslationString => providerTranslationString!.Translations)
                .Include(order => order.Status)
                .ThenInclude(status => status!.Name)
                .ThenInclude(statusTranslationString => statusTranslationString!.Translations)
                .Include(order => order.OrderProducts)
                .ThenInclude(orderProduct => orderProduct.Product)
                .ThenInclude(product => product!.Name)
                .ThenInclude(productTranslationString => productTranslationString!.Translations)
                .Include(order => order.Transactions)
                .AsSplitQuery();
        }

        var domainEntity = await query.FirstOrDefaultAsync(domainEntity => domainEntity.Id == id);
        return Mapper.Map(domainEntity);
    }
}