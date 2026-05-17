using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Catalogue.Domain.IRepositories;

public interface IMovieRepository
{
    Task AddMovie(Movie movie);
    Task<IEnumerable<Movie>> GetMovies(Guid cinemaId); 
    Task<Movie> GetMovie(Guid id, Guid cinemaId);
    Task UpdateMovie(Movie movie);
    Task DeleteMovie(Movie movie);
}