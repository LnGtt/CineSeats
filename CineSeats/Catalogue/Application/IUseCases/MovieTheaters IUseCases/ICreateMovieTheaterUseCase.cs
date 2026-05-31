using CineSeats.Catalogue.Application.DTOs.MovieTheaters_DTOs;
namespace CineSeats.Catalogue.Application.IUseCases.MovieTheaters_IUseCases;

public interface ICreateMovieTheaterUseCase
{
    Task ExecuteAsync(CreateMovieTheaterRequest request);
}