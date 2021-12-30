using AutoMapper;
using Base.DAL.EF.Mappers;

namespace WebApp.DAL.DTO.AutoMapper.Mappers;

public class OrderProductMapper : BaseMapper<WebApp.DAL.DTO.OrderProduct, WebApp.Domain.OrderProduct>
{
    public OrderProductMapper(IMapper mapper) : base(mapper)
    {
            
    }
}