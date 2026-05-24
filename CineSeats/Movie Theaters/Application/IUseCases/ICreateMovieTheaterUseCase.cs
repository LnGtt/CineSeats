using CineSeats.Movie_Theaters.Application.DTOs;
namespace CineSeats.Movie_Theaters.Application.IUseCases;

public interface ICreateMovieTheaterUseCase
{
    Task ExecuteAsync(CreateMovieTheaterRequest request);
}