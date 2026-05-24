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

    public async Task ExecuteAsync(CreateMovieTheaterRequest request)
    {
        var email = new EmailVO(request.Email);
        var password = new PasswordVO(request.Password);

        var domainRooms = request.Rooms.Select(r =>
        {
            // Passa o número da sala e conta quantos elementos vieram na lista do request
            return new Room(r.Number, r.Seats.Count);
        }).ToList();
        
        var movieTheater = new MovieTheater(request.Name, domainRooms, email, password);
        
        await _repository.AddAsync(movieTheater);
    }
    
    
}