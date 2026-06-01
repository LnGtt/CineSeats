using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CineSeats.Catalogue.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly Context_Post _context;

        public MovieRepository(Context_Post context)
        {
            _context = context;
        }

        public async Task AddMovie(Movie movie)
        {
            await _context.Movie.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _context.Movie.ToListAsync();
        }

        public async Task<Movie> GetMovie(Guid id)
        {
            return await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateMovie(Movie movie)
        {
            _context.Movie.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovie(Movie movie)
        {
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
        }
    }
}
