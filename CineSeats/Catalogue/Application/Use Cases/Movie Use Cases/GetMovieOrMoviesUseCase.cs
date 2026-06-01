using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Movie_Use_Cases;

public class GetMovieOrMoviesUseCase : IGetMovieOrMoviesUseCase
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieOrMoviesUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task<IEnumerable<GetMovieResponse>> Run()
    {
        var movies = await _movieRepository.GetMovies();

        return movies.Select(m => new GetMovieResponse
        {
            Id = m.Id,
            Title = m.Title,
            DurationMinutes = m.DurationMinutes,
            StartDate = m.StartDate,
            EndDate = m.EndDate
        });
    }
    
    public async Task<GetMovieResponse> Run(Guid id)
    {
        var movie = await _movieRepository.GetMovie(id)
                    ?? throw new KeyNotFoundException("Movie Not Found");

        return new GetMovieResponse
        {
            Id = movie.Id,
            Title = movie.Title,
            DurationMinutes = movie.DurationMinutes,
            StartDate = movie.StartDate,
            EndDate = movie.EndDate
        };
    }
}