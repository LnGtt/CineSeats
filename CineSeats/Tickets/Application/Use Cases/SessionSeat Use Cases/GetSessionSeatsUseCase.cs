using CineSeats.Tickets.Application.DTOs.SessionSeat_DTOs;
using CineSeats.Tickets.Application.IUseCases.SessionSeat_IUseCases;
using CineSeats.Tickets.Domain.Enums;
using CineSeats.Tickets.Domain.IRepositories;

namespace CineSeats.Tickets.Application.Use_Cases.SessionSeat_Use_Cases;

public class GetSessionSeatsUseCase : IGetSessionSeatsUseCase
{
    private readonly ISessionSeatRepository _sessionSeatRepository;

    public GetSessionSeatsUseCase(ISessionSeatRepository sessionSeatRepository)
    {
        _sessionSeatRepository = sessionSeatRepository;
    }
    
    public async Task<IEnumerable<SessionSeatResponse>> Run(Guid sessionId)
    {
        var seats = await _sessionSeatRepository.GetSeatsBySessionId(sessionId);

        var response = seats.Select(seat => new SessionSeatResponse
        {
            Id = seat.Id,
            SessionId = seat.SessionId,
            SeatNumber = seat.SeatNumber,
            Status = seat.Status switch
            {
                SeatStatus.Available => "Disponível",
                SeatStatus.Reserved => "Reservado",
                SeatStatus.Sold => "Vendido",
                _ => "Desconhecido"
            }
        }).ToList();

        return response;
    }
}