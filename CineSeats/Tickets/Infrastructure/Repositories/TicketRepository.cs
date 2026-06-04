using CineSeats.Infrastructure;
using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CineSeats.Tickets.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly Context_Post _context;
    public TicketRepository(Context_Post context)
    {
        _context = context;
    }
    
    public async Task AddTicket(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
    }

    public Task RemoveTicket(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        return Task.CompletedTask; 
    }

    public Task UpdateTicket(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        return Task.CompletedTask;
    }

    public async Task<Ticket> GetTicket(Guid id)
    {
        return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByOrderId(Guid orderId)
    {
        return await _context.Tickets
            .Where(t => t.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetTicketsBySessionId(Guid sessionId)
    {
        return await _context.Tickets
            .Where(t => t.SessionId == sessionId)
            .ToListAsync();
    }
}