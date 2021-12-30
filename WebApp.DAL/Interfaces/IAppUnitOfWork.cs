using Base.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.DAL.Interfaces;

public interface IAppUnitOfWork : IBaseUnitOfWork
{
    ICategoryRepository Categories { get; }
    ILocationRepository Locations { get; }
    IOrderProductRepository OrderProducts { get; }
    IOrderRepository Orders { get; }
    IPictureRepository Pictures { get; }
    IProductRepository Products { get; }
    IProviderRepository Providers { get; }
    IStatusRepository Statuses { get; }
    ITransactionRepository Transactions { get; }
}