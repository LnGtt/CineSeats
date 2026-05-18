using Microsoft.AspNetCore.Mvc;
using CineSeats.Movie_Theaters.Application.DTOs;
using CineSeats.Movie_Theaters.Application.IUseCases;
using CineSeats.Movie_Theaters.Domain.Entities;

namespace CineSeats.Movie_Theaters.Presentation.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class MovieTheaterController : ControllerBase
{
    private readonly ICreateMovieTheaterUseCase _useCase;

    public MovieTheaterController(ICreateMovieTheaterUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateMovieTheaterRequest request)
    {
        try
        {
            _useCase.Execute(request);
            return StatusCode(201, "Cinema cadastrado com sucesso!");
        }
        catch (AggregateException ex)
        {
            return BadRequest(ex.InnerException.Message);
        }
    }
}