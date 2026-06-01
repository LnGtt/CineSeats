using CineSeats.Catalogue.Application.DTOs.Room_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Room_Use_Cases;

public class GetRoomOrRoomsUseCase : IGetRoomOrRoomsUseCase
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomOrRoomsUseCase(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<GetRoomDetailResponse> Run(Guid id)
    {
        var room = await _roomRepository.GetRoomById(id)
                   ?? throw new KeyNotFoundException("Room Not Found");

        return new GetRoomDetailResponse
        {
            Id = room.Id,
            RoomNumber = room.RoomNumber,
            TotalCapacity = room.TotalCapacity,
            Layout = room.Layout.Select(row => new RowMapDTO
            {
                RowLetter = row.RowLetter,
                NumberOfSeats = row.NumberOfSeats
            }).ToList()
        };
    }
    
    public async Task<IEnumerable<GetRoomsResponse>> Run()
    {
        var rooms = await _roomRepository.GetRooms();

        return rooms.Select(room => new GetRoomsResponse
        {
            Id = room.Id,
            RoomNumber = room.RoomNumber,
            TotalCapacity = room.TotalCapacity
        });
    }
}