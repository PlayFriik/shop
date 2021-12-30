using System.Threading.Tasks;
using Base.DAL;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseUnitOfWork<TDbContext> : BaseUnitOfWork
    where TDbContext: DbContext
{
    protected readonly TDbContext BaseDbContext;
        
    public BaseUnitOfWork(TDbContext baseDbContext)
    {
        BaseDbContext = baseDbContext;
    }
        
    public override Task<int> SaveChangesAsync()
    {
        return BaseDbContext.SaveChangesAsync();
    }
}