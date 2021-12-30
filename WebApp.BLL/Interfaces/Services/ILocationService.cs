using Base.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Interfaces.Services;

public interface ILocationService : IBaseService<WebApp.BLL.DTO.Location, WebApp.DAL.DTO.Location>, ILocationRepositoryCustom<WebApp.BLL.DTO.Location>
{
        
}