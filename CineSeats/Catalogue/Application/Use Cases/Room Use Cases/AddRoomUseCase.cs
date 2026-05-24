using CineSeats.Catalogue.Application.DTOs.Room_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;
using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Catalogue.ValueObject;

namespace CineSeats.Catalogue.Application.Use_Cases.Room_Use_Cases;

public class AddRoomUseCase : IAddRoomUseCase
{
    private readonly IRoomRepository _roomRepository;

    public AddRoomUseCase(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task Run(AddRoomRequest request)
    {
        var layout = request.Layout.Select(dto => new RowMap(dto.RowLetter, dto.NumberOfSeats));
        var room = new Room(request.CinemaId, request.RoomNumber, layout);
        
        await _roomRepository.AddRoom(room);
    }
}