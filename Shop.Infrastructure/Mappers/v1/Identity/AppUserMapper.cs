using AutoMapper;
using Base.Infrastructure.Mappers;
using Shop.Application.Models.v1.Identity;

namespace Shop.Infrastructure.Mappers.v1.Identity;

public class AppUserMapper : BaseMapper<AppUser, Domain.Models.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}