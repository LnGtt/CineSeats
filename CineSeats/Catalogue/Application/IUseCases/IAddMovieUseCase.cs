using CineSeats.Catalogue.Application.DTOs;

namespace CineSeats.Catalogue.Application.IUseCases;

public interface IAddMovieUseCase
{
    Task Run(AddMovieRequest request);
}