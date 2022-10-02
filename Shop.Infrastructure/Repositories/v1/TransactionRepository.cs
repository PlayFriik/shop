using AutoMapper;
using Base.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;

namespace Shop.Infrastructure.Repositories.v1;

public class TransactionRepository : BaseRepository<Transaction, AppDbContext, Domain.Models.Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<IEnumerable<Transaction>> ToListAsync(Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);
        query = query
            .Include(transaction => transaction.Order);

        var domainEntities = await query.ToListAsync();
        return domainEntities.Select(domainEntity => Mapper.Map(domainEntity));
    }

    public override async Task<Transaction?> FirstOrDefaultAsync(Guid id, bool include, Guid? userId = default, bool noTracking = true)
    {
        var query = GetQuery(userId, noTracking);

        if (include)
        {
            query = query
                .Include(transaction => transaction.Order);
        }

        var domainEntity = await query.FirstOrDefaultAsync(domainEntity => domainEntity.Id == id);
        return Mapper.Map(domainEntity);
    }
}