using Base.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Interfaces.Services;

public interface IProviderService : IBaseService<WebApp.BLL.DTO.Provider, WebApp.DAL.DTO.Provider>, IProviderRepositoryCustom<WebApp.BLL.DTO.Provider>
{
        
}