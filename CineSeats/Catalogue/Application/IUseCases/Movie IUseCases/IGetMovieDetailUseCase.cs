using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;

public interface IGetMovieDetailUseCase
{
    Task<GetMovieDetailResponse> Run(Guid id, Guid cinemaId);
}