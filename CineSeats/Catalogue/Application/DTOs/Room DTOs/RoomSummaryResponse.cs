namespace CineSeats.Catalogue.Application.DTOs.Room_DTOs;

public class RoomSummaryResponse
{
    public Guid Id { get; set; }
    public int RoomNumber { get; set; }
    public int TotalCapacity { get; set; }
}