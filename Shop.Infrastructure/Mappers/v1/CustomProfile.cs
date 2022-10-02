using AutoMapper;
using Shop.Application.Models.v1;
using Shop.Application.Models.v1.Identity;

namespace Shop.Infrastructure.Mappers.v1;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<Category, Domain.Models.Category>().ReverseMap();
        CreateMap<Location, Domain.Models.Location>().ReverseMap();
        CreateMap<Order, Domain.Models.Order>().ReverseMap();
        CreateMap<OrderProduct, Domain.Models.OrderProduct>().ReverseMap();
        CreateMap<Picture, Domain.Models.Picture>().ReverseMap();
        CreateMap<Product, Domain.Models.Product>().ReverseMap();
        CreateMap<Provider, Domain.Models.Provider>().ReverseMap();
        CreateMap<Status, Domain.Models.Status>().ReverseMap();
        CreateMap<Transaction, Domain.Models.Transaction>().ReverseMap();
        CreateMap<AppUser, Domain.Models.Identity.AppUser>().ReverseMap();
    }
}