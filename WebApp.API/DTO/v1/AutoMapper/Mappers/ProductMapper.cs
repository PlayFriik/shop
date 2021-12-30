using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers;

public class ProductMapper : BaseMapper<WebApp.API.DTO.v1.Product, WebApp.BLL.DTO.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
            
    }
}