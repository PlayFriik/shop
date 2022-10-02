using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1;

namespace Shop.Infrastructure.Mappers.v1;

public class ProductMapper : BaseMapper<Product, Domain.Models.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
    }
}