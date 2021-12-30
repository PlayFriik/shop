using Base.DAL.Interfaces.Repositories;
using WebApp.DAL.DTO;

namespace WebApp.DAL.Interfaces.Repositories;

public interface ILocationRepository : IBaseRepository<Location>, ILocationRepositoryCustom<Location>
{
        
}
    
public interface ILocationRepositoryCustom<TEntity>
{
        
}