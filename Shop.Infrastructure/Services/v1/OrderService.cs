using Base.Infrastructure.Services;
using Shop.Application.Models.v1;
using Shop.Application.Repositories.v1;
using Shop.Application.Services.v1;

namespace Shop.Infrastructure.Services.v1;

public class OrderService : BaseService<Order, IOrderRepository>, IOrderService
{
    public OrderService(IOrderRepository orderRepository) : base(orderRepository)
    {
    }
}