using CineSeats.Catalogue.Application.DTOs;
using CineSeats.Catalogue.Application.IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases;

public class ListMoviesUseCase : IListMoviesUseCase
{
    private readonly IMovieRepository _movieRepository;

    public ListMoviesUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task<IEnumerable<GetMovieSummaryResponse>> Run(Guid cinemaId)
    {
        var movies = await _movieRepository.GetMovies(cinemaId);
        
        // O LINQ transforma a lista de domínio em lista de DTOs
        return movies.Select(m => new GetMovieSummaryResponse
        {
            Id = m.Id,
            Name = m.Name,
            AgeRestriction = m.AgeRestriction,
            Duration = m.Duration,
            Genres = m.Genres.ToList() // IReadOnlyCollection convertido para List
        });
    }
}