using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class ProviderMapper : BaseMapper<WebApp.API.DTO.v1.Provider, WebApp.BLL.DTO.Provider>
{
    public ProviderMapper(IMapper mapper) : base(mapper)
    {
            
    }
}