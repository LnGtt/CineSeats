using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DBContext_Mongo _database;

        public MovieRepository()
        {
        }

        public async Task AddMovie(Movie newMovie)
        {
            _database.Movie.AddAsync(newMovie);
            await _database.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _database.Movie.ToListAsync();
        }

        public async Task<Movie> GetMovie(Guid id)
        {
            return await _database.Movie.FindAsync(id);
        }

        public async Task UpdateMovie(Movie updatedMovie)
        {
            _database.Movie.Update(updatedMovie);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteMovie(Movie movie)
        {
            _database.Movie.DeleteAsync(movie);
            await _database.SaveChangesAsync();
        }
    }
}
