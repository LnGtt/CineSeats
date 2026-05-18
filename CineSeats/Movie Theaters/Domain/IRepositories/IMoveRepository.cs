using CineSeats.Movie_Theaters.Domain.Entities;
namespace CineSeats.Movie_Theaters.Domain.IRepositories;

public interface IMoveRepository
{
    MovieTheater FindById(Guid id);
    MovieTheater Save(MovieTheater movieTheater);
    void Add(MovieTheater movieTheater);
    void Delete(Guid id);
    void Update(MovieTheater movieTheater);
}