namespace CineSeats.Catalogue.Application.DTOs.Session_DTOs;

public class UpdateSessionStartTimeRequest
{
    public Guid Id { get; set; }
    public TimeOnly? StartTime { get; set; }
}