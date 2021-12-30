using AutoMapper;

namespace WebApp.DAL.DTO.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<WebApp.DAL.DTO.Category, WebApp.Domain.Category>().ReverseMap();
        CreateMap<WebApp.DAL.DTO.Location, WebApp.Domain.Location>().ReverseMap();
        CreateMap<WebApp.DAL.DTO.Order, WebApp.Domain.Order>().ReverseMap();
        CreateMap<WebApp.DAL.DTO.OrderProduct, WebApp.Domain.OrderProduct>().ReverseMap();
        CreateMap<WebApp.DAL.DTO.Picture, WebApp.Domain.Picture>().ReverseMap();
        CreateMap<WebApp.DAL.DTO.Product, WebApp.Domain.Product>().ReverseMap();
        CreateMap<WebApp.DAL.DTO.Provider, WebApp.Domain.Provider>().ReverseMap();
        CreateMap<WebApp.DAL.DTO.Status, WebApp.Domain.Status>().ReverseMap();
        CreateMap<WebApp.DAL.DTO.Transaction, WebApp.Domain.Transaction>().ReverseMap();
    }
}