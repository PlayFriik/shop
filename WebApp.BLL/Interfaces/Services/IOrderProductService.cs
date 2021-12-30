using Base.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Interfaces.Services;

public interface IOrderProductService : IBaseService<WebApp.BLL.DTO.OrderProduct, WebApp.DAL.DTO.OrderProduct>, IOrderProductRepositoryCustom<WebApp.BLL.DTO.OrderProduct>
{
        
}