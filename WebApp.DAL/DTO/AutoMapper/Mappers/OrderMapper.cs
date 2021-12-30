using AutoMapper;
using Base.DAL.EF.Mappers;

namespace WebApp.DAL.DTO.AutoMapper.Mappers;

public class OrderMapper : BaseMapper<WebApp.DAL.DTO.Order, WebApp.Domain.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
            
    }
}