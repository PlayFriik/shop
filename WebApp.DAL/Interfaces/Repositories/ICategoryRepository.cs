using Base.DAL.Interfaces.Repositories;
using WebApp.DAL.DTO;

namespace WebApp.DAL.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>, ICategoryRepositoryCustom<Category>
{
        
}
    
public interface ICategoryRepositoryCustom<TEntity>
{
        
}