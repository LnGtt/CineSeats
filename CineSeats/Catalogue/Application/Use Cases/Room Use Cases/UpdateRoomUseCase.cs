using CineSeats.Catalogue.Application.DTOs.Room_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;
using CineSeats.Catalogue.ValueObject;

namespace CineSeats.Catalogue.Application.Use_Cases.Room_Use_Cases;

public class UpdateRoomUseCase : IUpdateRoomUseCase
{
    private readonly IRoomRepository _roomRepository;
    public UpdateRoomUseCase(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task Run(UpdateRoomRequest request)
    {
        var room = await _roomRepository.GetRoomById(request.Id, request.CinemaId);

        if (room == null)
            throw new KeyNotFoundException("Sala não encontrada ou não pertence a este cinema.");
        
        var newLayout = request.Layout.Select(dto => new RowMap(dto.RowLetter, dto.NumberOfSeats));
        room.UpdateDetails(request.RoomNumber, newLayout);
        await _roomRepository.UpdateRoom(room);
    }
}