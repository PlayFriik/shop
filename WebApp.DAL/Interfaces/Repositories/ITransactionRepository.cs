using Base.DAL.Interfaces.Repositories;
using WebApp.DAL.DTO;

namespace WebApp.DAL.Interfaces.Repositories;

public interface ITransactionRepository : IBaseRepository<Transaction>, ITransactionRepositoryCustom<Transaction>
{
        
}
    
public interface ITransactionRepositoryCustom<TEntity>
{
        
}