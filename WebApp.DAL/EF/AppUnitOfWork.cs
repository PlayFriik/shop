using AutoMapper;
using Base.DAL.EF;
using WebApp.DAL.EF.Repositories;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.DAL.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        private IMapper _mapper;
        
        public AppUnitOfWork(AppDbContext appDbContext, IMapper mapper) : base(appDbContext)
        {
            _mapper = mapper;
        }

        public ICategoryRepository Categories => GetRepository(() =>
            new CategoryRepository(BaseDbContext, _mapper));
        
        public ILocationRepository Locations =>
            GetRepository(() => new LocationRepository(BaseDbContext, _mapper));
        
        public IOrderProductRepository OrderProducts =>
            GetRepository(() => new OrderProductRepository(BaseDbContext, _mapper));
        
        public IOrderRepository Orders =>
            GetRepository(() => new OrderRepository(BaseDbContext, _mapper));
        
        public IPictureRepository Pictures =>
            GetRepository(() => new PictureRepository(BaseDbContext, _mapper));
        
        public IProductRepository Products =>
            GetRepository(() => new ProductRepository(BaseDbContext, _mapper));
        
        public IProviderRepository Providers =>
            GetRepository(() => new ProviderRepository(BaseDbContext, _mapper));
            
        public IStatusRepository Statuses =>
            GetRepository(() => new StatusRepository(BaseDbContext, _mapper));

        public ITransactionRepository Transactions =>
            GetRepository(() => new TransactionRepository(BaseDbContext, _mapper));
    }
}
