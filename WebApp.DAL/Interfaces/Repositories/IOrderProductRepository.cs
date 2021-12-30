using Base.DAL.Interfaces.Repositories;
using WebApp.DAL.DTO;

namespace WebApp.DAL.Interfaces.Repositories;

public interface IOrderProductRepository : IBaseRepository<OrderProduct>, IOrderProductRepositoryCustom<OrderProduct>
{
        
}
    
public interface IOrderProductRepositoryCustom<TEntity>
{
        
}