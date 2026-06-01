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
    
    public async Task Run(Guid id)
    {
        var movie = await _movieRepository.GetMovie(id)
                    ?? throw new KeyNotFoundException("Movie Not Found");

        await _movieRepository.DeleteMovie(movie);
    }
}