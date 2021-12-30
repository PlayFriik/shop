using AutoMapper;
using Base.BLL.Mappers;

namespace WebApp.BLL.DTO.AutoMapper.Mappers;

public class StatusMapper : BaseMapper<WebApp.BLL.DTO.Status, WebApp.DAL.DTO.Status>
{
    public StatusMapper(IMapper mapper) : base(mapper)
    {
            
    }
}