using Base.Infrastructure.Services;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;
using Shop.Application.Services.v1;

namespace Shop.Infrastructure.Services.v1;

public class ProductService : BaseService<Product, IProductRepository>, IProductService
{
    public ProductService(IProductRepository productRepository) : base(productRepository)
    {
    }

    public async Task<IEnumerable<Product>> ToListAsync(Guid? categoryId, string? sortBy, Guid? userId = default, bool noTracking = true)
    {
        var entities = await Repository.ToListAsync(userId, noTracking);

        if (categoryId != null)
        {
            entities = entities.Where(product => product.CategoryId == categoryId).ToList();
        }

        if (sortBy != null)
        {
            switch (sortBy.ToLower())
            {
                case "best-sellers":
                    entities = entities.OrderBy(product => product.Sold).Reverse().ToList();
                    break;
                case "title-ascending":
                    entities = entities.OrderBy(product => product.Name!.ToString()).ToList();
                    break;
                case "title-descending":
                    entities = entities.OrderBy(product => product.Name!.ToString()).Reverse().ToList();
                    break;
                case "price-ascending":
                    entities = entities.OrderBy(product => product.Price).ToList();
                    break;
                case "price-descending":
                    entities = entities.OrderBy(product => product.Price).Reverse().ToList();
                    break;
            }
        }

        return entities;
    }
}