using CineSeats.Catalogue.Domain.Entities;

namespace CineSeats.Catalogue.Domain.IRepositories;

public interface IMovieTheaterRepository
{
    MovieTheater FindById(Guid id);
    MovieTheater Save(MovieTheater movieTheater);
    Task AddAsync(MovieTheater movieTheater);
    Task<MovieTheater?> GetByIdAsync(Guid id);
    void Delete(Guid id);
    void Update(MovieTheater movieTheater);
}