using AutoMapper;
using Base.BLL.Mappers;

namespace WebApp.BLL.DTO.AutoMapper.Mappers;

public class LocationMapper : BaseMapper<WebApp.BLL.DTO.Location, WebApp.DAL.DTO.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
            
    }
}