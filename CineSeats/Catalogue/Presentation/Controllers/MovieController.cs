using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace CineSeats.Catalogue.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IAddMovieUseCase _addMovieUseCase;
    private readonly IGetMovieOrMoviesUseCase _getMovieOrMoviesUseCase;
    private readonly IUpdateMovieUseCase _updateMovieUseCase;
    private readonly IDeleteMovieUseCase _deleteMovieUseCase;

    public MovieController(
        IAddMovieUseCase addMovieUseCase,
        IGetMovieOrMoviesUseCase getMovieOrMoviesUseCase,
        IUpdateMovieUseCase updateMovieUseCase,
        IDeleteMovieUseCase deleteMovieUseCase)
    {
        _addMovieUseCase = addMovieUseCase;
        _getMovieOrMoviesUseCase = getMovieOrMoviesUseCase;
        _updateMovieUseCase = updateMovieUseCase;
        _deleteMovieUseCase = deleteMovieUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddMovieRequest request)
    {
        try
        {
            await _addMovieUseCase.Run(request);
            return StatusCode(201);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao cadastrar o filme." });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var movies = await _getMovieOrMoviesUseCase.Run();
            
            if (!movies.Any())
                return NoContent();

            return Ok(movies);
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao listar o catálogo de filmes." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var movie = await _getMovieOrMoviesUseCase.Run(id);
            return Ok(movie);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao buscar os detalhes do filme." });
        }
    }

    [HttpPut("{id}/details")]
    public async Task<IActionResult> UpdateDetails(Guid id, [FromBody] UpdateMovieDetailsRequest request)
    {
        try
        {
            request.Id = id;
            await _updateMovieUseCase.Run(request);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao atualizar os dados técnicos do filme." });
        }
    }

    [HttpPut("{id}/schedule")]
    public async Task<IActionResult> UpdateSchedule(Guid id, [FromBody] UpdateMovieScheduleRequest request)
    {
        try
        {
            request.Id = id;
            await _updateMovieUseCase.Run(request);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao reagendar as datas do filme." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteMovieUseCase.Run(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao remover o filme." });
        }
    }
    
}