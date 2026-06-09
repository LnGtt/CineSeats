namespace CineSeats.Catalogue.Domain.Entities;

public class Session
{
    public Guid Id { get; private set; }
    public Guid MovieId { get; private set; }
    public Guid RoomId { get; private set; }
    public string Description { get; private set; }
    public TimeOnly? StartTime { get; private set; }
    public decimal TicketPrice { get; private set; }

    protected Session() { }

    public Session(Guid movieId, Guid roomId, string description, TimeOnly? startTime, decimal ticketPrice)
    {
        if (movieId == Guid.Empty)
            throw new ArgumentException("Movie Id cannot be empty");

        if (roomId == Guid.Empty)
            throw new ArgumentException("Room Id cannot be empty");

        if (string.IsNullOrEmpty(description))
            throw new ArgumentException("Description cannot be null or empty");

        if (startTime == null)
            throw new ArgumentException("Start time cannot be null");
        
        if (ticketPrice <= 0)
            throw new ArgumentException("Ticket price can't be zero or negative");

        Id = Guid.NewGuid();
        MovieId = movieId;
        RoomId = roomId;
        Description = description;
        StartTime = startTime;
        TicketPrice = ticketPrice;
    }
    
    public void UpdateStartTime(TimeOnly? startTime)
    {
        if (startTime == null)
            throw new ArgumentException("Start time cannot be null");
        
        StartTime = startTime;
    }

    public void ChangeRoom(Guid newRoom)
    {
        if (newRoom == Guid.Empty)
            throw new ArgumentException("Room Id cannot be empty");
        
        RoomId = newRoom;
    }

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrEmpty(newDescription))
            throw new ArgumentException("Description cannot be null or empty");
        
        Description = newDescription;
    }

    public void UpdateTicketPrice(decimal newTicketPrice)
    {
        if (newTicketPrice <= 0)
            throw new ArgumentException("Ticket price can't be zero or negative");
        
        TicketPrice = newTicketPrice;
    }
}