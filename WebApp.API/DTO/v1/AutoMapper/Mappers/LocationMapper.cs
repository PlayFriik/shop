using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class LocationMapper : BaseMapper<WebApp.API.DTO.v1.Location, WebApp.BLL.DTO.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
            
    }
}