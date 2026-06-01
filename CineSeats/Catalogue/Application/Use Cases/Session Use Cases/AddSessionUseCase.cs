using CineSeats.Catalogue.Application.DTOs.Session_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Session_Use_Cases;

public class AddSessionUseCase : IAddSessionUseCase
{
    private readonly ISessionRepository _sessionRepository;

    public AddSessionUseCase(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task Run(AddSessionRequest request)
    {
        var session = new Session(
            request.MovieId,
            request.RoomId,
            request.Description,
            request.StartTime,
            request.TicketPrice
        );

        await _sessionRepository.AddSession(session);
    }
}