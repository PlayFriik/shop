using System;
using System.Threading.Tasks;

namespace Base.DAL.Interfaces;

public interface IBaseUnitOfWork
{
    Task<int> SaveChangesAsync();

    TRepository GetRepository<TRepository>(Func<TRepository> instantiation)
        where TRepository : class;
}