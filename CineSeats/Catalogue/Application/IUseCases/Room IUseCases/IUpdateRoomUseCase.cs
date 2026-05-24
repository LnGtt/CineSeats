using CineSeats.Catalogue.Application.DTOs.Room_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;

public interface IUpdateRoomUseCase
{
    Task Run(UpdateRoomRequest request);
}