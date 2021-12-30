using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Base.BLL.Services;
using WebApp.BLL.DTO;
using WebApp.BLL.DTO.AutoMapper.Mappers;
using WebApp.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Services;

public class ProductService : BaseService<IAppUnitOfWork, IProductRepository, Product, WebApp.DAL.DTO.Product>, IProductService
{
    public ProductService(IAppUnitOfWork serviceUow, IProductRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ProductMapper(mapper))
    {
            
    }
        
    public async Task<IEnumerable<Product>> ToListSortedAsync(Guid? categoryId, string? sortBy, Guid userId = default, bool noTracking = true)
    {
        return (await ServiceRepository.ToListSortedAsync(categoryId, sortBy, userId, noTracking)).Select(entity => Mapper.Map(entity))!;
    }
}