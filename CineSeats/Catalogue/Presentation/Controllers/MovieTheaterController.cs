using Microsoft.AspNetCore.Mvc;
using CineSeats.Catalogue.Application.IUseCases.MovieTheaters_IUseCases;
using CineSeats.Catalogue.Application.DTOs.MovieTheaters_DTOs;


namespace CineSeats.Catalogue.Presentation.Controllers;



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
            _useCase.ExecuteAsync(request);
            return StatusCode(201, "Cinema cadastrado com sucesso!");
        }
        catch (AggregateException ex)
        {
            return BadRequest(ex.InnerException.Message);
        }
    }
}