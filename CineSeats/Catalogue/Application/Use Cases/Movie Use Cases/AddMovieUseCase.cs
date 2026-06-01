using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;
using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Movie_Use_Cases;

public class AddMovieUseCase : IAddMovieUseCase
{
    private readonly IMovieRepository _movieRepository;
    
    public AddMovieUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task Run(AddMovieRequest request)
    {
        var movie = new Movie(
            request.Title,
            request.DurationMinutes,
            request.StartDate,
            request.EndDate
        );

        await _movieRepository.AddMovie(movie);
    }
}