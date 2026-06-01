namespace CineSeats.Catalogue.Application.DTOs.Room_DTOs;

public class UpdateRoomRequest
{
    public Guid Id { get; set; }
    public int RoomNumber { get; set; }
    public List<RowMapDTO> Layout { get; set; }
}