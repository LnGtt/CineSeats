using CineSeats.Catalogue.Application.DTOs;
using CineSeats.Catalogue.Application.IUseCases;
using CineSeats.Catalogue.Domain.IRepositories;

namespace CineSeats.Catalogue.Application.Use_Cases;

public class GetMovieDetailUseCase : IGetMovieDetailUseCase
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieDetailUseCase(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task<GetMovieDetailResponse> Run(Guid id, Guid cinemaId)
    {
        // 1. Busca com segurança Multi-Tenant
        var movie = await _movieRepository.GetMovie(id, cinemaId);
        
        if (movie == null)
            throw new KeyNotFoundException("Filme não encontrado ou não pertence a este cinema.");
        
        return new GetMovieDetailResponse
        {
            Id = movie.Id,
            Name = movie.Name,
            AgeRestriction = movie.AgeRestriction,
            Synopsis = movie.Synopsis,
            Director = movie.Director,
            Producer = movie.Producer,
            Duration = movie.Duration,
            Genres = movie.Genres.ToList(),
            // Converte o IReadOnlyDictionary interno da entidade de volta para Dictionary do DTO
            Cast = movie.Cast.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) 
        };
    }
}