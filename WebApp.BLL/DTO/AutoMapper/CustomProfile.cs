using AutoMapper;

namespace WebApp.BLL.DTO.AutoMapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<WebApp.BLL.DTO.Category, WebApp.DAL.DTO.Category>().ReverseMap();
        CreateMap<WebApp.BLL.DTO.Location, WebApp.DAL.DTO.Location>().ReverseMap();
        CreateMap<WebApp.BLL.DTO.Order, WebApp.DAL.DTO.Order>().ReverseMap();
        CreateMap<WebApp.BLL.DTO.OrderProduct, WebApp.DAL.DTO.OrderProduct>().ReverseMap();
        CreateMap<WebApp.BLL.DTO.Picture, WebApp.DAL.DTO.Picture>().ReverseMap();
        CreateMap<WebApp.BLL.DTO.Product, WebApp.DAL.DTO.Product>().ReverseMap();
        CreateMap<WebApp.BLL.DTO.Provider, WebApp.DAL.DTO.Provider>().ReverseMap();
        CreateMap<WebApp.BLL.DTO.Status, WebApp.DAL.DTO.Status>().ReverseMap();
        CreateMap<WebApp.BLL.DTO.Transaction, WebApp.DAL.DTO.Transaction>().ReverseMap();
    }
}