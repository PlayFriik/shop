using AutoMapper;
using Base.BLL.Mappers;

namespace WebApp.BLL.DTO.AutoMapper.Mappers;

public class PictureMapper : BaseMapper<WebApp.BLL.DTO.Picture, WebApp.DAL.DTO.Picture>
{
    public PictureMapper(IMapper mapper) : base(mapper)
    {
            
    }
}