using CineSeats.Catalogue.Application.DTOs.Room_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Room_Use_Cases;

public class ListRoomsUseCase : IListRoomsUseCase
{
    private readonly IRoomRepository _roomRepository;
    public ListRoomsUseCase(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<IEnumerable<RoomSummaryResponse>> Run(Guid cinemaId)
    {
        var rooms = await _roomRepository.GetRoomsByCinema(cinemaId);
        
        return rooms.Select(r => new RoomSummaryResponse
        {
            Id = r.Id,
            RoomNumber = r.RoomNumber,
            TotalCapacity = r.TotalCapacity
        });
    }
}