namespace CineSeats.Movie_Theaters.Domain.Entities;

public class Room
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public List<int> Seats { get; set; }
    
    public Room(int number, int seats)
    {
        try
        {
            if (seats < 10)
            {
                throw new ArgumentException("The number of seats cannot be less than 10");
            }
        
            if (number <= 0)
            {
                throw new ArgumentNullException("Room number cannot be zero or negative");
            }
            
            Id = Guid.NewGuid();
            this.Number = number;
            this.Seats = new List<int>();
            for (int i = 1; i < seats; i++)
            {
                Seats.Add(i);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    
}