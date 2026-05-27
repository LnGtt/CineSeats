namespace CineSeats.Catalogue.Domain.Entities;

public class Session
{
    public Guid Id { get; private set; }
    public Guid MovieId { get; private set; }
    public Guid RoomId { get; private set; }
    public string Description { get; private set; } //exemplo: Sessão 19:30 CineMais sala 3
    public TimeOnly? StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }

    public Session(Guid movieId, Guid roomId, string description, TimeOnly? startTime)
    {
        if (movieId == Guid.Empty)
            throw new ArgumentException("Movie Id cannot be empty");

        if (roomId == Guid.Empty)
            throw new ArgumentException("Room Id cannot be empty");

        if (description == null)
            throw new ArgumentException("Description cannot be null");

        if (startTime == null)
            throw new ArgumentException("Start Time cannot be null");

        Id = Guid.NewGuid();
        MovieId = movieId;
        RoomId = roomId;
        Description = description;
        StartTime = startTime;
        
        /*if (Movie.duration <= 1:30h)
              EndTime = startTime + 1:30h
          else if (<= 2h)
              EndTime = startTime + 2h
          else
              EndTime = startTime + 2:30h
        */  
    }

}