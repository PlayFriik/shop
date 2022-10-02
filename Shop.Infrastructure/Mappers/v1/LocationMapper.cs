using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1;

namespace Shop.Infrastructure.Mappers.v1;

public class LocationMapper : BaseMapper<Location, Domain.Models.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}