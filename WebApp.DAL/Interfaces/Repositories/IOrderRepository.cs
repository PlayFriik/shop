using Base.DAL.Interfaces.Repositories;
using WebApp.DAL.DTO;

namespace WebApp.DAL.Interfaces.Repositories;

public interface IOrderRepository : IBaseRepository<Order>, IOrderRepositoryCustom<Order>
{
        
}
    
public interface IOrderRepositoryCustom<TEntity>
{
        
}