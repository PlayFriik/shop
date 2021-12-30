using AutoMapper;
using Base.DAL.EF.Mappers;

namespace WebApp.DAL.DTO.AutoMapper.Mappers;

public class StatusMapper : BaseMapper<WebApp.DAL.DTO.Status, WebApp.Domain.Status>
{
    public StatusMapper(IMapper mapper) : base(mapper)
    {
            
    }
}