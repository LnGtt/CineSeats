using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;

public interface IListMoviesUseCase
{
    Task<IEnumerable<GetMovieResponse>> Run(Guid cinemaId);
}