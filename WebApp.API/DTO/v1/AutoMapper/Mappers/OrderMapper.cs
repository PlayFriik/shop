using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class OrderMapper : BaseMapper<WebApp.API.DTO.v1.Order, WebApp.BLL.DTO.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
            
    }
}