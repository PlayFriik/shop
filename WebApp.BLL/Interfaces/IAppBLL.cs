using Base.BLL.Contracts;
using WebApp.BLL.Interfaces.Services;

namespace WebApp.BLL.Interfaces;

public interface IAppBLL : IBaseBLL
{
    ICategoryService Categories { get; }
    ILocationService Locations { get; }
    IOrderService Orders { get; }
    IOrderProductService OrderProducts { get; }
    IPictureService Pictures { get; }
    IProductService Products { get; }
    IProviderService Providers { get; }
    IStatusService Statuses { get; }
    ITransactionService Transactions { get; }
}