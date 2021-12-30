using AutoMapper;
using Base.BLL.Mappers;

namespace WebApp.BLL.DTO.AutoMapper.Mappers;

public class ProductMapper : BaseMapper<WebApp.BLL.DTO.Product, WebApp.DAL.DTO.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
            
    }
}