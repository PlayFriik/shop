using AutoMapper;
using Base.BLL;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Interfaces.Services;
using WebApp.BLL.Services;
using WebApp.DAL.Interfaces;

namespace WebApp.BLL;

public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
{
    private readonly IMapper _mapper;
        
    public AppBLL(IAppUnitOfWork appUnitOfWork, IMapper mapper) : base(appUnitOfWork)
    {
        _mapper = mapper;
    }

    public ICategoryService Categories =>
        GetService<ICategoryService>(() => new CategoryService(BaseUnitOfWork, BaseUnitOfWork.Categories, _mapper));
        
    public ILocationService Locations =>
        GetService<ILocationService>(() => new LocationService(BaseUnitOfWork, BaseUnitOfWork.Locations, _mapper));
        
    public IOrderService Orders =>
        GetService<IOrderService>(() => new OrderService(BaseUnitOfWork, BaseUnitOfWork.Orders, _mapper));
        
    public IOrderProductService OrderProducts =>
        GetService<IOrderProductService>(() => new OrderProductService(BaseUnitOfWork, BaseUnitOfWork.OrderProducts, _mapper));
        
    public IPictureService Pictures =>
        GetService<IPictureService>(() => new PictureService(BaseUnitOfWork, BaseUnitOfWork.Pictures, _mapper));
        
    public IProductService Products =>
        GetService<IProductService>(() => new ProductService(BaseUnitOfWork, BaseUnitOfWork.Products, _mapper));
        
    public IProviderService Providers =>
        GetService<IProviderService>(() => new ProviderService(BaseUnitOfWork, BaseUnitOfWork.Providers, _mapper));
        
    public IStatusService Statuses =>
        GetService<IStatusService>(() => new StatusService(BaseUnitOfWork, BaseUnitOfWork.Statuses, _mapper));

    public ITransactionService Transactions =>
        GetService<ITransactionService>(() => new TransactionService(BaseUnitOfWork, BaseUnitOfWork.Transactions, _mapper));
}