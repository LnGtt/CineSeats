namespace CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;

public interface IDeleteMovieUseCase
{
    Task Run(Guid id);
}