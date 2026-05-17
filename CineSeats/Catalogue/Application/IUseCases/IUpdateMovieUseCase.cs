using CineSeats.Catalogue.Application.DTOs;

namespace CineSeats.Catalogue.Application.IUseCases;

public interface IUpdateMovieUseCase
{
    Task Run(UpdateMovieRequest request);
}