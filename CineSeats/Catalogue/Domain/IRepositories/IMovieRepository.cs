using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Catalogue.Domain.IRepositories;

public interface IMovieRepository
{
    Task AddMovie(Movie movie);
    Task<IEnumerable<Movie>> GetMovies(); 
    Task<Movie> GetMovie(Guid id);
    Task UpdateMovie(Movie movie);
    Task DeleteMovie(Movie movie);
}