using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1;

namespace Shop.Infrastructure.Mappers.v1;

public class StatusMapper : BaseMapper<Status, Domain.Models.Status>
{
    public StatusMapper(IMapper mapper) : base(mapper)
    {
    }
}