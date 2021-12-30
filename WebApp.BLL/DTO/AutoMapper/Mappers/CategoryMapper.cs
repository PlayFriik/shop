using AutoMapper;
using Base.BLL.Mappers;

namespace WebApp.BLL.DTO.AutoMapper.Mappers;

public class CategoryMapper : BaseMapper<WebApp.BLL.DTO.Category, WebApp.DAL.DTO.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
            
    }
}