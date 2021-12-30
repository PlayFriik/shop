using AutoMapper;
using Base.DAL.EF.Mappers;

namespace WebApp.DAL.DTO.AutoMapper.Mappers;

public class PictureMapper : BaseMapper<WebApp.DAL.DTO.Picture, WebApp.Domain.Picture>
{
    public PictureMapper(IMapper mapper) : base(mapper)
    {
            
    }
}