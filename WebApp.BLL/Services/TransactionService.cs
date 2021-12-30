using AutoMapper;
using Base.BLL.Services;
using WebApp.BLL.DTO;
using WebApp.BLL.DTO.AutoMapper.Mappers;
using WebApp.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Services;

public class TransactionService : BaseService<IAppUnitOfWork, ITransactionRepository, Transaction, WebApp.DAL.DTO.Transaction>, ITransactionService
{
    public TransactionService(IAppUnitOfWork serviceUow, ITransactionRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new TransactionMapper(mapper))
    {
            
    }
}