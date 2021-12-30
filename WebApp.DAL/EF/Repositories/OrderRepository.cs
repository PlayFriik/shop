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

public class OrderRepository : BaseRepository<AppDbContext, WebApp.DAL.DTO.Order, WebApp.Domain.Order>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new OrderMapper(mapper))
    {
            
    }
        
    public override async Task<IEnumerable<WebApp.DAL.DTO.Order>> ToListAsync(Guid userId = default, bool noTracking = true)
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
        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity));

        return dalEntities!;
    }

    public override async Task<WebApp.DAL.DTO.Order?> FirstOrDefaultAsync(Guid id, bool include, Guid userId = default, bool noTracking = true)
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

        return Mapper.Map(await query.FirstOrDefaultAsync(dalEntity => dalEntity.Id == id));
    }
}