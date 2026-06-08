using CineSeats.Tickets.Application.DTOs.SessionSeat_DTOs;
using CineSeats.Tickets.Application.IUseCases.SessionSeat_IUseCases;
using CineSeats.Tickets.Application.IUseCases.Integration_IUseCases;
using CineSeats.Tickets.Domain.Entities;
using CineSeats.Tickets.Domain.Enums;
using CineSeats.Tickets.Domain.IRepositories;

namespace CineSeats.Tickets.Application.Use_Cases.SessionSeat_Use_Cases;

public class GetSessionSeatsUseCase : IGetSessionSeatsUseCase
{
    private readonly ISessionSeatRepository _sessionSeatRepository;
    private readonly ICatalogueService _catalogueService;
    private readonly IUnitOfWork _unitOfWork;

    public GetSessionSeatsUseCase(
        ISessionSeatRepository sessionSeatRepository,
        ICatalogueService catalogueService,
        IUnitOfWork unitOfWork)
    {
        _sessionSeatRepository = sessionSeatRepository;
        _catalogueService = catalogueService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<SessionSeatResponse>> Run(Guid sessionId)
    {
        var seats = await _sessionSeatRepository.GetSeatsBySessionId(sessionId);

        // Se não houver assentos configurados para essa sessão, nós os geramos de acordo com o layout da sala
        if (!seats.Any())
        {
            var seatNumbers = await _catalogueService.GetSessionSeatNumbers(sessionId);
            if (seatNumbers.Any())
            {
                foreach (var seatNumber in seatNumbers)
                {
                    var newSeat = new SessionSeat(sessionId, seatNumber);
                    await _sessionSeatRepository.AddSessionSeat(newSeat);
                }
                await _unitOfWork.Commit();
                seats = await _sessionSeatRepository.GetSeatsBySessionId(sessionId);
            }
        }

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