using AutoMapper;
using Base.BLL.Mappers;

namespace WebApp.BLL.DTO.AutoMapper.Mappers;

public class ProviderMapper : BaseMapper<WebApp.BLL.DTO.Provider, WebApp.DAL.DTO.Provider>
{
    public ProviderMapper(IMapper mapper) : base(mapper)
    {
            
    }
}