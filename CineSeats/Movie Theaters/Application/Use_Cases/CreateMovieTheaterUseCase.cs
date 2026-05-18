using CineSeats.Movie_Theaters.Application.DTOs;
using CineSeats.Movie_Theaters.Application.IUseCases;
using CineSeats.Movie_Theaters.Domain.Entities;
using CineSeats.Movie_Theaters.Domain.IRepositories;
using CineSeats.Movie_Theaters.ValueObject;

namespace CineSeats.Movie_Theaters.Application.Use_Cases;

public class CreateMovieTheaterUseCase : ICreateMovieTheaterUseCase 
{
    private readonly IMoveRepository _repository;

    public CreateMovieTheaterUseCase(IMoveRepository repository)
    {
        _repository = repository;   
    }

    public void Execute(CreateMovieTheaterRequest request)
    {
        var email = new EmailVO(request.Email);
        var password = new PasswordVO(request.Password);
        
        var movieTheater = new MovieTheater(request.Name, request.Rooms, email, password);
        
        _repository.Add(movieTheater);
    }
}