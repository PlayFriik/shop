using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class CategoryMapper : BaseMapper<WebApp.API.DTO.v1.Category, WebApp.BLL.DTO.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
            
    }
}