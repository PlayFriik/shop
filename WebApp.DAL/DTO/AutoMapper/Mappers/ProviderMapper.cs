using AutoMapper;
using Base.DAL.EF.Mappers;

namespace WebApp.DAL.DTO.AutoMapper.Mappers;

public class ProviderMapper : BaseMapper<WebApp.DAL.DTO.Provider, WebApp.Domain.Provider>
{
    public ProviderMapper(IMapper mapper) : base(mapper)
    {
            
    }
}