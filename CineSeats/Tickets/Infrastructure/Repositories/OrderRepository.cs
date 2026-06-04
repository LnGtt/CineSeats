using CineSeats.Infrastructure;
using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.Enums;
using CineSeats.Tickets.Domain.IRepositories;
using CineSeats.Tickets.Value_Object;
using Microsoft.EntityFrameworkCore;

namespace CineSeats.Tickets.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly Context_Post _context;
    public OrderRepository(Context_Post context)
    {
        _context = context;
    }
    
    public async Task AddOrder(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<Order> GetOrder(Guid id)
    {
        return await _context.Orders
            // CRÍTICO: Sempre carregar os filhos do Agregado. 
            // Sem o Include, a lista _tickets virá vazia e o modelo de domínio vai quebrar.
            .Include(o => o.Tickets) 
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerEmail(CustomerEmailVO email)
    {
        return await _context.Orders
            .Include(o => o.Tickets)
            .Where(o => o.CustomerEmail.EmailAddress == email.EmailAddress)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus status)
    {
        return await _context.Orders
            .Include(o => o.Tickets)
            .Where(o => o.Status == status)
            .ToListAsync();
    }

    public async Task UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        
    }
}