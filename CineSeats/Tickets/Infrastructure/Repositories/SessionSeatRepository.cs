using CineSeats.Infrastructure;
using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CineSeats.Tickets.Infrastructure.Repositories;

public class SessionSeatRepository : ISessionSeatRepository
{
    private readonly Context_Post _context;
    public  SessionSeatRepository(Context_Post context)
    {
        _context = context;
    }
    
    public async Task AddSessionSeat(SessionSeat sessionSeat)
    {
        await _context.SessionSeats.AddAsync(sessionSeat);
    }

    public Task UpdateSessionSeat(SessionSeat sessionSeat)
    {
        // O EF Core rastreia a entidade. 
        // Quando o Unit of Work chamar o SaveChangesAsync(), o EF Core vai comparar 
        // o campo 'Version' que está em memória com o que está na base de dados.
        // Se forem diferentes (alguém comprou primeiro), lança a DbUpdateConcurrencyException!
        _context.SessionSeats.Update(sessionSeat);
        
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<SessionSeat>> GetSeatsBySessionId(Guid sessionId)
    {
        return await _context.SessionSeats
            .Where(s => s.SessionId == sessionId)
            .ToListAsync();
    }

    public async Task<IEnumerable<SessionSeat>> GetSpecificSeats(Guid sessionId, IEnumerable<string> seatNumbers)
    {
        return await _context.SessionSeats
            .Where(s => s.SessionId == sessionId && seatNumbers.Contains(s.SeatNumber))
            .ToListAsync();
    }
}