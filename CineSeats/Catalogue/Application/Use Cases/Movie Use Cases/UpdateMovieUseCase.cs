using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases.Movie_Use_Cases;

public class UpdateMovieUseCase : IUpdateMovieUseCase
{
    private readonly IMovieRepository _movieRepository;

    public UpdateMovieUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task Run(UpdateMovieDetailsRequest detailsRequest)
    {
        var movie = await _movieRepository.GetMovie(detailsRequest.Id)
                    ?? throw new KeyNotFoundException("Movie Not Found");

        movie.UpdateDetails(detailsRequest.Title, detailsRequest.DurationMinutes);

        await _movieRepository.UpdateMovie(movie);
    }
    
    public async Task Run(UpdateMovieScheduleRequest scheduleRequest)
    {
        var movie = await _movieRepository.GetMovie(scheduleRequest.Id)
                    ?? throw new KeyNotFoundException("Movie Not Found");

        movie.Reschedule(scheduleRequest.StartDate, scheduleRequest.EndDate);

        await _movieRepository.UpdateMovie(movie);
    }
}