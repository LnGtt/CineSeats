using CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Movie_Use_Cases;

public class DeleteMovieUseCase : IDeleteMovieUseCase
{
    private readonly IMovieRepository _movieRepository;

    public DeleteMovieUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task Run(Guid id, Guid cinemaId)
    {
        var movie = await _movieRepository.GetMovie(id, cinemaId);
        
        if (movie == null)
        {
            throw new KeyNotFoundException("Filme não encontrado ou não pertence a este cinema.");
        }
        
        await _movieRepository.DeleteMovie(movie);
    }
}