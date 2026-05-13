using CineSeats.Movie_Theaters.Domain.Entities;
namespace CineSeats.Movie_Theaters.Domain.ViewMovie;

public class ViewMovieTheater
{
    public ViewMovieTheater(string name, List<Room> rooms)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("name");
        }

        if (rooms == null)
        {
            throw new ArgumentNullException("rooms");
        }

        this.Name = name;
        this.Rooms = rooms;
    }
}