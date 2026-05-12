namespace CineSeats.Movie_Theaters.Domain.Entities;

public class Room
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public List<int> Seats { get; set; }

    public Room(int number, List<int> seats)
    {
        if (seats.Count < 10)
        {
            throw new ArgumentException("The number of seats cannot be less than 10");
        }

        if (number < 1 || number > 100)
        {
            throw new ArgumentException("The number must be between 1 and 100");
        }
        this.Number = number;
        this.Seats = seats;
    }
}