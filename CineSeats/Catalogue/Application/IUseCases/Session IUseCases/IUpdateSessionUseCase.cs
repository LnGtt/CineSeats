using CineSeats.Catalogue.Application.DTOs.Session_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;

public interface IUpdateSessionUseCase
{
    Task RunDetails(UpdateSessionDetailsRequest updateSessionDetailsRequest);
    Task RunRooms(UpdateSessionRoomRequest updateSessionRoomRequest);
    Task RunStartTime(UpdateSessionStartTimeRequest updateSessionStartTimeRequest);
}