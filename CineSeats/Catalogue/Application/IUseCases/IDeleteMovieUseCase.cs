namespace CineSeats.Catalogue.Application.IUseCases;

public interface IDeleteMovieUseCase
{
    Task Run(Guid id, Guid cinemaId);
}