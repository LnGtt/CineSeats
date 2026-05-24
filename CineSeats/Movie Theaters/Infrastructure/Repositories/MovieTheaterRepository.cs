using Microsoft.EntityFrameworkCore;
using CineSeats.infrestrutura;
using CineSeats.Movie_Theaters.Domain.Entities;
using CineSeats.Movie_Theaters.Domain.IRepositories;

namespace CineSeats.Movie_Theaters.Infrastructure.Repositories;

public class MovieTheaterRepository
{
    private readonly Context_Post _context;

    public MovieTheaterRepository(Context_Post context)
    {
        _context = context;
    }

    public async Task AddAsync(MovieTheater movieTheater)
    {
        await _context.MovieTheaters.AddAsync(movieTheater);
        await _context.SaveChangesAsync();
    }

    public async Task<MovieTheater?> GetByIdAsync(Guid id)
    {
        return await _context.MovieTheaters
            .Include(mt => mt.Rooms) // Carrega as salas junto com o cinema
            .FirstOrDefaultAsync(mt => mt.Id == id);
    }
}