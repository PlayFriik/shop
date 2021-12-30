using Base.DAL.Interfaces.Repositories;
using WebApp.DAL.DTO;

namespace WebApp.DAL.Interfaces.Repositories;

public interface IProviderRepository : IBaseRepository<Provider>, IProviderRepositoryCustom<Provider>
{
        
}
    
public interface IProviderRepositoryCustom<TEntity>
{
        
}