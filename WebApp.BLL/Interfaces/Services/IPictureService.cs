using Base.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Interfaces.Services;

public interface IPictureService : IBaseService<WebApp.BLL.DTO.Picture, WebApp.DAL.DTO.Picture>, IPictureRepositoryCustom<WebApp.BLL.DTO.Picture>
{
        
}