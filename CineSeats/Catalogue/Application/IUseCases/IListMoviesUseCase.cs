using CineSeats.Catalogue.Application.DTOs;

namespace CineSeats.Catalogue.Application.IUseCases;

public interface IListMoviesUseCase
{
    Task<IEnumerable<GetMovieSummaryResponse>> Run(Guid cinemaId);
}