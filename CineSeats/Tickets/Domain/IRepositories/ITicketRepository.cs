using CineSeats.Tickets.Domain.Entities;

namespace CineSeats.Tickets.Domain.IRepositories;

public interface ITicketRepository
{
    Task AddTicket(Ticket ticket);
    Task RemoveTicket(Ticket ticket);
    Task UpdateTicket(Ticket ticket);
    Task<Ticket> GetTicket(Guid id);
    Task<IEnumerable<Ticket>> GetTicketsByOrderId(Guid orderId);
    Task<IEnumerable<Ticket>> GetTicketsBySessionId(Guid sessionId);
}