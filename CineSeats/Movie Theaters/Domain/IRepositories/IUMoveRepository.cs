using CineSeats.Movie_Theaters.Domain.Entities;
namespace CineSeats.Movie_Theaters.Domain.IRepositories;

public interface IUMoveRepository
{
    public interface IMoveRepository
    {
        public MovieTheater FindById(Guid id);
        public MovieTheater save(MovieTheater movieTheater);
        public void Delete(Guid id);
        public void Update(MovieTheater movieTheater);
    }
}