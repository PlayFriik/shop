using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class StatusMapper : BaseMapper<WebApp.API.DTO.v1.Status, WebApp.BLL.DTO.Status>
{
    public StatusMapper(IMapper mapper) : base(mapper)
    {
            
    }
}