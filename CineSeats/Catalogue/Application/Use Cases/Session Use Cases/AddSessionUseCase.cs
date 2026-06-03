using CineSeats.Catalogue.Application.DTOs.Session_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Catalogue.Application.Use_Cases.Session_Use_Cases;

public class AddSessionUseCase : IAddSessionUseCase
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IRoomRepository _roomRepository;

    public AddSessionUseCase(
        ISessionRepository sessionRepository,
        IMovieRepository movieRepository,
        IRoomRepository roomRepository)
    {
        _sessionRepository = sessionRepository;
        _movieRepository = movieRepository;
        _roomRepository = roomRepository;
    }

    public async Task Run(AddSessionRequest request)
    {
        var movie = await _movieRepository.GetMovie(request.MovieId)
                    ?? throw new KeyNotFoundException("Movie not found");
        var room = await _roomRepository.GetRoomById(request.RoomId)
                   ?? throw new KeyNotFoundException("Room not found");
        var existingSessions = await _sessionRepository.GetSessionsByRoomId(request.RoomId);
        var newSessionStart = request.StartTime.Value;
        var newSessionEnd = newSessionStart.AddMinutes(movie.DurationMinutes);
        
        foreach (var existingSession in existingSessions)
        {
            if (existingSession.StartTime == null) continue;
            
            var existingMovie = await _movieRepository.GetMovie(existingSession.MovieId);
            if (existingMovie == null) continue;

            var existingStart = existingSession.StartTime.Value;
            var existingEnd = existingStart.AddMinutes(existingMovie.DurationMinutes);
            
            if (newSessionStart < existingEnd && newSessionEnd > existingStart)
            {
                throw new InvalidOperationException("There are already a session in this room at the same time");
            }
        }
        
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