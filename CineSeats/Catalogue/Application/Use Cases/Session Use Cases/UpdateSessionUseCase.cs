using CineSeats.Catalogue.Application.DTOs.Session_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Session_Use_Cases;

public class UpdateSessionUseCase : IUpdateSessionUseCase
{
    private readonly ISessionRepository _sessionRepository;

    public UpdateSessionUseCase(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task RunDetails(UpdateSessionDetailsRequest request)
    {
        var session = await _sessionRepository.GetSessionById(request.Id)
                      ?? throw new KeyNotFoundException("Session not found");

        session.UpdateDescription(request.Description);
        session.UpdateTicketPrice(request.TicketPrice);

        await _sessionRepository.UpdateSession(session);
    }

    public async Task RunRooms(UpdateSessionRoomRequest request)
    {
        var session = await _sessionRepository.GetSessionById(request.Id)
                      ?? throw new KeyNotFoundException("Session not found");
        
        session.ChangeRoom(request.RoomId);

        await _sessionRepository.UpdateSession(session);
    }

    public async Task RunStartTime(UpdateSessionStartTimeRequest request)
    {
        var session = await _sessionRepository.GetSessionById(request.Id)
                      ?? throw new KeyNotFoundException("Session not found");

        session.UpdateStartTime(request.StartTime);

        await _sessionRepository.UpdateSession(session);
    }
}