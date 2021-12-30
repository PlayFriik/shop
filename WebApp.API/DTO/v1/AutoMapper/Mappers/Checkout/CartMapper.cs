using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper.Mappers.Checkout;

public class CartMapper : BaseMapper<WebApp.API.DTO.v1.Checkout.Cart, BLL.DTO.Checkout.Cart>
{
    public CartMapper(IMapper mapper) : base(mapper)
    {
            
    }
}