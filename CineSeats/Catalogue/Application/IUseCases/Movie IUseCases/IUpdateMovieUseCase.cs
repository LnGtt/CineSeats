using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;

namespace CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;

public interface IUpdateMovieUseCase
{
    Task Run(UpdateMovieDetailsRequest detailsRequest);
    Task Run(UpdateMovieScheduleRequest scheduleRequest);
}