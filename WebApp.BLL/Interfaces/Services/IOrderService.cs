using System.Threading.Tasks;
using Base.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Interfaces.Services;

public interface IOrderService : IBaseService<WebApp.BLL.DTO.Order, WebApp.DAL.DTO.Order>, IOrderRepositoryCustom<WebApp.BLL.DTO.Order>
{
    Task<WebApp.BLL.DTO.Order> Process(WebApp.BLL.DTO.Checkout.Cart cart);
}