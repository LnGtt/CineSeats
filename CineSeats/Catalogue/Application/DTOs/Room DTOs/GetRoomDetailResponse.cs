namespace CineSeats.Catalogue.Application.DTOs.Room_DTOs;

public class GetRoomDetailResponse
{
    public Guid Id { get; set; }
    public int RoomNumber { get; set; }
    public int TotalCapacity { get; set; }
    public List<RowMapDTO> Layout { get; set; }
}