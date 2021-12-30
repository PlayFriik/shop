using AutoMapper;
using Base.BLL.Services;
using WebApp.BLL.DTO;
using WebApp.BLL.DTO.AutoMapper.Mappers;
using WebApp.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Services;

public class StatusService : BaseService<IAppUnitOfWork, IStatusRepository, Status, WebApp.DAL.DTO.Status>, IStatusService
{
    public StatusService(IAppUnitOfWork serviceUow, IStatusRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new StatusMapper(mapper))
    {
            
    }
}