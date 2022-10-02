using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1;

namespace Shop.Infrastructure.Mappers.v1;

public class OrderMapper : BaseMapper<Order, Domain.Models.Order>
{
    public OrderMapper(IMapper mapper) : base(mapper)
    {
    }
}