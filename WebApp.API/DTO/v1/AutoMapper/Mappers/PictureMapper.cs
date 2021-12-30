using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class PictureMapper : BaseMapper<WebApp.API.DTO.v1.Picture, WebApp.BLL.DTO.Picture>
{
    public PictureMapper(IMapper mapper) : base(mapper)
    {
            
    }
}