using AutoMapper;
using Base.DAL.EF.Mappers;

namespace WebApp.DAL.DTO.AutoMapper.Mappers;

public class ProductMapper : BaseMapper<WebApp.DAL.DTO.Product, WebApp.Domain.Product>
{
    public ProductMapper(IMapper mapper) : base(mapper)
    {
            
    }
}