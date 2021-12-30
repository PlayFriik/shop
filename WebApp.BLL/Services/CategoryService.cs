using AutoMapper;
using Base.BLL.Services;
using WebApp.BLL.DTO.AutoMapper.Mappers;
using WebApp.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Services;

public class CategoryService : BaseService<IAppUnitOfWork, ICategoryRepository, BLL.DTO.Category, WebApp.DAL.DTO.Category>, ICategoryService
{
    public CategoryService(IAppUnitOfWork serviceUow, ICategoryRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new CategoryMapper(mapper))
    {
            
    }
}