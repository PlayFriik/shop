using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers.Identity;

public class AppUserMapper : BaseMapper<WebApp.API.DTO.v1.Identity.AppUser, Domain.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
            
    }
}