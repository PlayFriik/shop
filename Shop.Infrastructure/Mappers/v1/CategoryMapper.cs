using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1;

namespace Shop.Infrastructure.Mappers.v1;

public class CategoryMapper : BaseMapper<Category, Domain.Models.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}