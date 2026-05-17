using CineSeats.Catalogue.Application.DTOs;
using CineSeats.Catalogue.Application.IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases;

public class UpdateMovieUseCase : IUpdateMovieUseCase
{
    private readonly IMovieRepository _movieRepository;

    public UpdateMovieUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task Run(UpdateMovieRequest request)
    {
        var movie = await _movieRepository.GetMovie(request.Id, request.CinemaId);

        if (movie == null)
            throw new KeyNotFoundException("Filme não encontrado ou não pertence a este cinema.");
        
        movie.UpdateDetails(
            request.Name,
            request.Genres,
            request.AgeRestriction,
            request.Synopsis,
            request.Cast,
            request.Director,
            request.Producer,
            request.Duration
        );
        
        await _movieRepository.UpdateMovie(movie);
    }
}