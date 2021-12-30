using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Base.BLL.Services;
using WebApp.BLL.DTO;
using WebApp.BLL.DTO.AutoMapper.Mappers;
using WebApp.BLL.DTO.Checkout;
using WebApp.BLL.Interfaces.Services;
using WebApp.DAL.Interfaces;
using WebApp.DAL.Interfaces.Repositories;

namespace WebApp.BLL.Services;

public class OrderService : BaseService<IAppUnitOfWork, IOrderRepository, Order, WebApp.DAL.DTO.Order>, IOrderService
{
    public OrderService(IAppUnitOfWork serviceUow, IOrderRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new OrderMapper(mapper))
    {
            
    }

    public async Task<Order> Process(Cart cart)
    {
        var status = (await ServiceUow.Statuses.ToListAsync()).FirstOrDefault();
        if (status == null)
        {
            status = new WebApp.DAL.DTO.Status
            {
                Id = Guid.NewGuid(),
                Name = "Processing"
            };
            ServiceUow.Statuses.Add(status);
        }
        
        var order = new WebApp.DAL.DTO.Order
        {
            Id = Guid.NewGuid(),
            AppUserId = cart.AppUserId,
            LocationId = cart.LocationId,
            StatusId = status.Id,
            DateTime = DateTime.Now,
            Total = cart.Entries.Sum(entry => entry.Total) + cart.ProviderPrice
        };
        ServiceUow.Orders.Add(order);
        
        foreach (var entry in cart.Entries)
        {
            var orderProduct = new WebApp.DAL.DTO.OrderProduct
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = entry.ProductId,
                Quantity = entry.Quantity
            };
            ServiceUow.OrderProducts.Add(orderProduct);
        
            var product = await ServiceUow.Products.FirstOrDefaultAsync(entry.ProductId, true);
            if (product == null)
            {
                continue;
            }
        
            product.Quantity -= entry.Quantity;
            product.Sold += entry.Quantity;
                        
            ServiceUow.Products.Update(product);
        }
                    
        var transaction = new WebApp.DAL.DTO.Transaction
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            Amount = cart.Entries.Sum(entry => entry.Total) + cart.ProviderPrice
        };
        ServiceUow.Transactions.Add(transaction);
                    
        await ServiceUow.SaveChangesAsync();
        
        return Mapper.Map(order)!;
    }
}