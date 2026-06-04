using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.Enums;
using CineSeats.Tickets.Value_Object;

namespace CineSeats.Tickets.Domain.IRepositories;

public interface IOrderRepository
{
    Task AddOrder(Order order);
    Task<Order> GetOrder(Guid id);
    Task<IEnumerable<Order>> GetOrdersByCustomerEmail(CustomerEmailVO email);
    Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus status);
    Task UpdateOrder(Order order);
}