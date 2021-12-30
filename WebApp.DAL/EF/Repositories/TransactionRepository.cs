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

public class TransactionRepository : BaseRepository<AppDbContext, WebApp.DAL.DTO.Transaction, WebApp.Domain.Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new TransactionMapper(mapper))
    {
            
    }
        
    public override async Task<IEnumerable<WebApp.DAL.DTO.Transaction>> ToListAsync(Guid userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(transaction => transaction.Order);

        var domainEntities = await query.ToListAsync();
        var dalEntities = domainEntities.Select(domainEntity => Mapper.Map(domainEntity));

        return dalEntities!;
    }

    public override async Task<WebApp.DAL.DTO.Transaction?> FirstOrDefaultAsync(Guid id, bool include, Guid userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(transaction => transaction.Order);
        }

        return Mapper.Map(await query.FirstOrDefaultAsync(dalEntity => dalEntity.Id == id));
    }
}