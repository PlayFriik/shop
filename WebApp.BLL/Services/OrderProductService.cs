using AutoMapper;
using Base.BLL.Services;
using WebApp.BLL.DTO;
using WebApp.BLL.DTO.AutoMapper.Mappers;
using WebApp.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Services;

public class OrderProductService : BaseService<IAppUnitOfWork, IOrderProductRepository, OrderProduct, WebApp.DAL.DTO.OrderProduct>, IOrderProductService
{
    public OrderProductService(IAppUnitOfWork serviceUow, IOrderProductRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new OrderProductMapper(mapper))
    {
            
    }
}