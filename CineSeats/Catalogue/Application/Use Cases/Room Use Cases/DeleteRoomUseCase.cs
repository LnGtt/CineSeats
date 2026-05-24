using CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CineSeats.Catalogue.Application.Use_Cases.Room_Use_Cases;

public class DeleteRoomUseCase : IDeleteRoomUseCase
{
    private readonly IRoomRepository _roomRepository;

    public DeleteRoomUseCase(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task Run(Guid id, Guid cinemaId)
    {
        var room = await _roomRepository.GetRoomById(id, cinemaId);
        
        if (room == null)
            throw new KeyNotFoundException("Sala não encontrada ou não pertence a este cinema.");
        
        await _roomRepository.DeleteRoom(room);
    }
}