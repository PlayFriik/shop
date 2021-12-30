using AutoMapper;
using Base.BLL.Services;
using WebApp.BLL.DTO;
using WebApp.BLL.DTO.AutoMapper.Mappers;
using WebApp.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Services;

public class ProviderService : BaseService<IAppUnitOfWork, IProviderRepository, Provider, WebApp.DAL.DTO.Provider>, IProviderService
{
    public ProviderService(IAppUnitOfWork serviceUow, IProviderRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ProviderMapper(mapper))
    {
            
    }
}