using AutoMapper;
using Base.BLL.Mappers;

namespace WebApp.BLL.DTO.AutoMapper.Mappers;

public class OrderMapper : BaseMapper<WebApp.BLL.DTO.Order, WebApp.DAL.DTO.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
            
    }
}