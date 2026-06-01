using CineSeats.Catalogue.Application.DTOs.Room_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Room_Use_Cases;

public class GetRoomDetailUseCase : IGetRoomDetailUseCase
{
    private readonly IRoomRepository _roomRepository;
    public GetRoomDetailUseCase(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<GetRoomDetailResponse> Run(Guid id, Guid cinemaId)
    {
        var room = await _roomRepository.GetRoomById(id, cinemaId);
        
        if (room == null)
            throw new KeyNotFoundException("Sala não encontrada ou não pertence a este cinema.");
        
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
}