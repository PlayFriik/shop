using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DAL.Interfaces.Repositories;
using WebApp.DAL.DTO;

namespace WebApp.DAL.Interfaces.Repositories;

public interface IProductRepository : IBaseRepository<Product>, IProductRepositoryCustom<Product>
{
        
}
    
public interface IProductRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> ToListSortedAsync(Guid? categoryId, string? sortBy, Guid userId = default, bool noTracking = true);
}