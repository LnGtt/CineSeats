using CineSeats.Tickets.Application.DTOs.SessionSeat_DTOs;

namespace CineSeats.Tickets.Application.IUseCases.SessionSeat_IUseCases;

public interface IGetSessionSeatsUseCase
{
    Task<IEnumerable<SessionSeatResponse>> Run(Guid sessionId);
}