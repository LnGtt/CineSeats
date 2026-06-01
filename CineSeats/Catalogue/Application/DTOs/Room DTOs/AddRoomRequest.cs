namespace CineSeats.Catalogue.Application.DTOs.Room_DTOs;

public class AddRoomRequest
{
    public int RoomNumber { get; set; }
    public List<RowMapDTO> Layout { get; set; }
}