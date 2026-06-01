using CineSeats.Catalogue.Application.DTOs.Room_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;

public interface IGetRoomDetailUseCase
{
    Task<GetRoomDetailResponse> Run(Guid id, Guid cinemaId);
}