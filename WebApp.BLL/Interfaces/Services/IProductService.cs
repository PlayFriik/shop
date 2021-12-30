using Base.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Interfaces.Services;

public interface IProductService : IBaseService<WebApp.BLL.DTO.Product, WebApp.DAL.DTO.Product>, IProductRepositoryCustom<WebApp.BLL.DTO.Product>
{
        
}