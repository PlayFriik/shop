using Base.DAL.Interfaces.Repositories;
using WebApp.DAL.DTO;

namespace WebApp.DAL.Interfaces.Repositories;

public interface IStatusRepository : IBaseRepository<Status>, IStatusRepositoryCustom<Status>
{
        
}
    
public interface IStatusRepositoryCustom<TEntity>
{
        
}