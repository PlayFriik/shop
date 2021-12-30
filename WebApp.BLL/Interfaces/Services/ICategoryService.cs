using Base.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Interfaces.Services;

public interface ICategoryService : IBaseService<WebApp.BLL.DTO.Category, WebApp.DAL.DTO.Category>, ICategoryRepositoryCustom<WebApp.BLL.DTO.Category>
{
        
}