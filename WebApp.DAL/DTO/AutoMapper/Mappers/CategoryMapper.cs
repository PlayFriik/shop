using AutoMapper;
using Base.DAL.EF.Mappers;

namespace WebApp.DAL.DTO.AutoMapper.Mappers;

public class CategoryMapper : BaseMapper<WebApp.DAL.DTO.Category, WebApp.Domain.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
            
    }
}