namespace CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;

public interface IDeleteRoomUseCase
{
    Task Run(Guid id, Guid cinemaId);
}