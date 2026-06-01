using CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Session_Use_Cases;

public class DeleteSessionUseCase : IDeleteSessionUseCase
{
    private readonly ISessionRepository _sessionRepository;

    public DeleteSessionUseCase(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task Run(Guid id)
    {
        var session = await _sessionRepository.GetSessionById(id)
                      ?? throw new KeyNotFoundException("Session not found");

        await _sessionRepository.DeleteSession(session);
    }
}