using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class OrderProductMapper : BaseMapper<WebApp.API.DTO.v1.OrderProduct, WebApp.BLL.DTO.OrderProduct>
{
    public OrderProductMapper(IMapper mapper) : base(mapper)
    {
            
    }
}