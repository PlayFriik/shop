using AutoMapper;
using Base.DAL.EF.Mappers;

namespace WebApp.DAL.DTO.AutoMapper.Mappers;

public class LocationMapper : BaseMapper<WebApp.DAL.DTO.Location, WebApp.Domain.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
            
    }
}