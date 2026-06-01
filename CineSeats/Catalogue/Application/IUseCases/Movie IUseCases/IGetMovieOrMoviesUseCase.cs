using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;

public interface IGetMovieOrMoviesUseCase
{
    Task<IEnumerable<GetMovieResponse>> Run();
    Task<GetMovieResponse> Run(Guid id);
}