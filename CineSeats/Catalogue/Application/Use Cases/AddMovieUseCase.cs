using CineSeats.Catalogue.Application.DTOs;
using CineSeats.Catalogue.Application.IUseCases;
using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases;

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
            request.CinemaId,
            request.Name,
            request.Genres,
            request.AgeRestriction,
            request.Synopsis,
            request.Cast,
            request.Director,
            request.Producer,
            request.Duration
        );
        
        await _movieRepository.AddMovie(movie);
    }
}