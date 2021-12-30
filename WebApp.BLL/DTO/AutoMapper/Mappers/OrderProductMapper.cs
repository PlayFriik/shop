using AutoMapper;
using Base.BLL.Mappers;

namespace WebApp.BLL.DTO.AutoMapper.Mappers;

public class OrderProductMapper : BaseMapper<WebApp.BLL.DTO.OrderProduct, WebApp.DAL.DTO.OrderProduct>
{
    public OrderProductMapper(IMapper mapper) : base(mapper)
    {
            
    }
}