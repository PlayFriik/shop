using AutoMapper;
using Base.BLL.Services;
using WebApp.BLL.DTO;
using WebApp.BLL.DTO.AutoMapper.Mappers;
using WebApp.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Services;

public class PictureService : BaseService<IAppUnitOfWork, IPictureRepository, Picture, WebApp.DAL.DTO.Picture>, IPictureService
{
    public PictureService(IAppUnitOfWork serviceUow, IPictureRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PictureMapper(mapper))
    {
            
    }
}