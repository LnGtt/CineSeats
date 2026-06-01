namespace CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;

public interface IDeleteSessionUseCase
{
    Task Run(Guid id);
}