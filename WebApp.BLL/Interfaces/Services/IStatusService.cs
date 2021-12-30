using Base.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Interfaces.Services;

public interface IStatusService : IBaseService<WebApp.BLL.DTO.Status, WebApp.DAL.DTO.Status>, IStatusRepositoryCustom<WebApp.BLL.DTO.Status>
{
        
}