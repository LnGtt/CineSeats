using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Infrastructure;

namespace CineSeats.Catalogue.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly Context_Post _context;

    public SessionRepository(Context_Post context)
    {
        _context = context;
    }
    
    public async Task AddSession(Session session)
    {
        await _context.Session.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Session>> GetSessionsByRoomId(Guid roomId)
    {
        return await _context.Session
            .Where(s => s.RoomId == roomId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetSessionsByMovie(Guid movieId)
    {
        return await _context.Session
            .Where(s => s.MovieId == movieId)
            .ToListAsync();
    }

    public async Task<Session> GetSessionById(Guid id)
    {
        return await _context.Session.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task UpdateSession(Session session)
    {
        _context.Session.Update(session);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSession(Session session)
    {
        _context.Session.Remove(session);
        await _context.SaveChangesAsync();
    }
}