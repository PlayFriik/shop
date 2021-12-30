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

public class OrderProductRepository : BaseRepository<AppDbContext, WebApp.DAL.DTO.OrderProduct, WebApp.Domain.OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new OrderProductMapper(mapper))
    {
            
    }
        
    public override async Task<IEnumerable<WebApp.DAL.DTO.OrderProduct>> ToListAsync(Guid userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(orderProduct => orderProduct.Order)
            .Include(orderProduct => orderProduct.Product)
            .ThenInclude(product => product!.Name)
            .ThenInclude(nameTranslationString => nameTranslationString!.Translations);

        var domainEntities = await query.ToListAsync();
        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity));

        return dalEntities!;
    }

    public override async Task<WebApp.DAL.DTO.OrderProduct?> FirstOrDefaultAsync(Guid id, bool include, Guid userId = default, bool noTracking = true)
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

        return Mapper.Map(await query.FirstOrDefaultAsync(dalEntity => dalEntity.Id == id));
    }
}