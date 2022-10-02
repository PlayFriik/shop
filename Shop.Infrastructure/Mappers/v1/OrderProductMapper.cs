using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1;

namespace Shop.Infrastructure.Mappers.v1;

public class OrderProductMapper : BaseMapper<OrderProduct, Domain.Models.OrderProduct>
{
    public OrderProductMapper(IMapper mapper) : base(mapper)
    {
    }
}