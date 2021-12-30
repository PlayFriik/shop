using Base.DAL.Interfaces.Repositories;
using WebApp.DAL.DTO;

namespace WebApp.DAL.Interfaces.Repositories;

public interface IPictureRepository : IBaseRepository<Picture>, IPictureRepositoryCustom<Picture>
{
        
}
    
public interface IPictureRepositoryCustom<TEntity>
{
        
}