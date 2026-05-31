using CineSeats.Catalogue.Application.Use_Cases.MovieTheaters_Use_Case;
using CineSeats.Catalogue.Domain.Entities;
using CineSeats.Catalogue.Application.IUseCases.MovieTheaters_IUseCases;
using CineSeats.Catalogue.ValueObject;
using CineSeats.Catalogue.Application.DTOs.MovieTheaters_DTOs;
using CineSeats.Catalogue.Domain.IRepositories;


namespace CineSeats.Catalogue.Application.Use_Cases.MovieTheaters_Use_Case;

public class CreateMovieTheaterUseCase : ICreateMovieTheaterUseCase 
{
    private readonly IMovieTheaterRepository _repository;

    public CreateMovieTheaterUseCase(IMovieTheaterRepository repository)
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