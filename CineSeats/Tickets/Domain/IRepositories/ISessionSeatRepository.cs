using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.Enums;

namespace CineSeats.Tickets.Domain.IRepositories;

public interface ISessionSeatRepository
{
    Task AddSessionSeat(SessionSeat sessionSeat);
    Task UpdateSessionSeat(SessionSeat sessionSeat);
    Task<IEnumerable<SessionSeat>> GetSeatsBySessionId(Guid sessionId);
    Task<IEnumerable<SessionSeat>> GetSpecificSeats(Guid sessionId, IEnumerable<string> seatNumbers);
}