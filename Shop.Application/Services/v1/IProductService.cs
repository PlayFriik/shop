using Shop.Application.Models.v1;
using Base.Application.Services;

namespace Shop.Application.Services.v1;

public interface IProductService : IBaseService<Product>
{
    public Task<IEnumerable<Product>> ToListAsync(Guid? categoryId, string? sortBy, Guid? userId = default, bool noTracking = true);
}