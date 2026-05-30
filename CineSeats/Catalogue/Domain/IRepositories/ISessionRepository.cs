using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Catalogue.Domain.IRepositories;

public interface ISessionRepository
{
    Task AddSession(Session session);
    Task<IEnumerable<Session>> GetSessionsByRoomId(Guid roomId);
    Task<IEnumerable<Session>> GetSessionsByMovie(Guid movieId);
    Task<Session> GetSessionById(Guid id);
    Task UpdateSession(Session session);
    Task DeleteSession(Session session);
}