using System;
using System.Threading.Tasks;

namespace Base.BLL.Contracts
{
    public interface IBaseBLL
    {
        Task<int> SaveChangesAsync();
        
        TService GetService<TService>(Func<TService> instantiation)
            where TService : class;
    }
}