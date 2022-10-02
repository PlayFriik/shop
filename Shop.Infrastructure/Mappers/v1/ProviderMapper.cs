using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1;

namespace Shop.Infrastructure.Mappers.v1;

public class ProviderMapper : BaseMapper<Provider, Domain.Models.Provider>
{
    public ProviderMapper(IMapper mapper) : base(mapper)
    {
    }
}