using AutoMapper;

namespace WebApp.API.DTO.v1.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<WebApp.API.DTO.v1.Checkout.Cart, WebApp.BLL.DTO.Checkout.Cart>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Checkout.Entry, WebApp.BLL.DTO.Checkout.Entry>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Category, WebApp.BLL.DTO.Category>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Location, WebApp.BLL.DTO.Location>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Order, WebApp.BLL.DTO.Order>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.OrderProduct, WebApp.BLL.DTO.OrderProduct>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Picture, WebApp.BLL.DTO.Picture>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Product, WebApp.BLL.DTO.Product>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Provider, WebApp.BLL.DTO.Provider>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Status, WebApp.BLL.DTO.Status>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Transaction, WebApp.BLL.DTO.Transaction>().ReverseMap();
        CreateMap<WebApp.API.DTO.v1.Identity.AppUser, Domain.Identity.AppUser>().ReverseMap();
    }
}