using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Infrastructure;
using Microsoft.EntityFrameworkCore;

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
        await _context.SessionsCatalogo.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Session>> GetSessionsByRoomId(Guid roomId)
    {
        return await _context.SessionsCatalogo
            .Where(s => s.RoomId == roomId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetSessionsByMovie(Guid movieId)
    {
        return await _context.SessionsCatalogo
            .Where(s => s.MovieId == movieId)
            .ToListAsync();
    }
    
    public async Task<Session> GetSessionById(Guid id)
    {
        return await _context.SessionsCatalogo.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task UpdateSession(Session session)
    {
        _context.SessionsCatalogo.Update(session);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSession(Session session)
    {
        _context.SessionsCatalogo.Remove(session);
        await _context.SaveChangesAsync();
    }
}