using CineSeats.Catalogue.Application.DTOs.Session_DTOs;
using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;

public interface IGetSessionOrSessionsUseCase
{
    Task<IEnumerable<GetSessionsResponse>> GetSessionsByMovieId(Guid movieId);
    Task<IEnumerable<GetSessionsResponse>> GetSessionsByRoomId(Guid roomId);
    Task<GetSessionsResponse> GetSessionById(Guid id);
}