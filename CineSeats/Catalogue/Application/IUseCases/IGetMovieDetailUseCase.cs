using CineSeats.Catalogue.Application.DTOs;

namespace CineSeats.Catalogue.Application.IUseCases;

public interface IGetMovieDetailUseCase
{
    Task<GetMovieDetailResponse> Run(Guid id, Guid cinemaId);
}