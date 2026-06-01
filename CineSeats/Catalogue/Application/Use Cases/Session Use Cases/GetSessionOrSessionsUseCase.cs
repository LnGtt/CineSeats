using CineSeats.Catalogue.Application.DTOs.Session_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Session_Use_Cases;

public class GetSessionOrSessionsUseCase : IGetSessionOrSessionsUseCase
{
    private readonly ISessionRepository _sessionRepository;

    public GetSessionOrSessionsUseCase(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<GetSessionsResponse> GetSessionById(Guid id)
    {
        var session = await _sessionRepository.GetSessionById(id)
                      ?? throw new KeyNotFoundException("Session not found");

        return new GetSessionsResponse
        {
            Id = session.Id,
            MovieId = session.MovieId,
            RoomId = session.RoomId,
            Description = session.Description,
            StartTime = session.StartTime,
            TicketPrice = session.TicketPrice
        };
    }

    public async Task<IEnumerable<GetSessionsResponse>> GetSessionsByMovieId(Guid movieId)
    {
        var sessions = await _sessionRepository.GetSessionsByMovie(movieId);

        return sessions.Select(s => new GetSessionsResponse
        {
            Id = s.Id,
            MovieId = s.MovieId,
            RoomId = s.RoomId,
            Description = s.Description,
            StartTime = s.StartTime,
            TicketPrice = s.TicketPrice
        });
    }

    public async Task<IEnumerable<GetSessionsResponse>> GetSessionsByRoomId(Guid roomId)
    {
        var sessions = await _sessionRepository.GetSessionsByRoomId(roomId);

        return sessions.Select(s => new GetSessionsResponse
        {
            Id = s.Id,
            MovieId = s.MovieId,
            RoomId = s.RoomId,
            Description = s.Description,
            StartTime = s.StartTime,
            TicketPrice = s.TicketPrice
        });
    }
}