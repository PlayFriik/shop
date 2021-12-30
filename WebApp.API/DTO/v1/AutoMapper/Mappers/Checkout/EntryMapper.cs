using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers.Checkout;

public class EntryMapper : BaseMapper<WebApp.API.DTO.v1.Checkout.Entry, BLL.DTO.Checkout.Entry>
{
    public EntryMapper(IMapper mapper) : base(mapper)
    {
            
    }
}