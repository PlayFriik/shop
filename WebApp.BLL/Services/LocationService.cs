using AutoMapper;
using Base.BLL.Services;
using WebApp.BLL.DTO;
using WebApp.BLL.DTO.AutoMapper.Mappers;
using WebApp.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Services;

public class LocationService : BaseService<IAppUnitOfWork, ILocationRepository, Location, WebApp.DAL.DTO.Location>, ILocationService
{
    public LocationService(IAppUnitOfWork serviceUow, ILocationRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new LocationMapper(mapper))
    {
            
    }
}