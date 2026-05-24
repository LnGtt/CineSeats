using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;

public interface IAddMovieUseCase
{
    Task Run(AddMovieRequest request);
}