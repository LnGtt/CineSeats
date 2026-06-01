using CineSeats.Catalogue.Application.DTOs.Movie_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Movie_IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace CineSeats.Catalogue.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IAddMovieUseCase _addMovieUseCase;
    private readonly IUpdateMovieUseCase _updateMovieUseCase;
    private readonly IListMoviesUseCase _listMoviesUseCase;
    private readonly IGetMovieDetailUseCase _getMovieDetailUseCase;
    private readonly IDeleteMovieUseCase _deleteMovieUseCase;

    public MovieController(IAddMovieUseCase addMovieUseCase, IUpdateMovieUseCase updateMovieUseCase, IListMoviesUseCase listMoviesUseCase, IGetMovieDetailUseCase getMovieDetailUseCase, IDeleteMovieUseCase deleteMovieUseCase)
    {
        _addMovieUseCase = addMovieUseCase;
        _updateMovieUseCase = updateMovieUseCase;
        _listMoviesUseCase = listMoviesUseCase;
        _getMovieDetailUseCase = getMovieDetailUseCase;
        _deleteMovieUseCase = deleteMovieUseCase;
    }

    [HttpPost("PostMovie")]
    public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest request)
    {
        try
        {
            // Em produção com autenticação, o CinemaId viria do token JWT do Admin:
            // request.CinemaId = Guid.Parse(User.FindFirst("CinemaId").Value);
            
            await _addMovieUseCase.Run(request);
            
            return StatusCode(201);
        }
        catch (ArgumentException ex)
        {
            return BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
    
    [HttpPut("PutMovieById")]
    public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] UpdateMovieDetailsRequest detailsRequest)
    {
        try
        {
            if (id != detailsRequest.Id)
                return BadRequest();

            await _updateMovieUseCase.Run(detailsRequest);
            
            return NoContent(); 
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
    
    [HttpGet("GetCatalogue")]
    public async Task<IActionResult> GetCatalogue([FromQuery] Guid cinemaId)
    {
        try
        {
            if (cinemaId == Guid.Empty)
                return BadRequest(new { error = "O CinemaId é obrigatório para listar os filmes." });

            var movies = await _listMoviesUseCase.Run(cinemaId);

            if (!movies.Any())
                return NoContent();
            
            return Ok(movies);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("GetMovieDetails")]
    public async Task<IActionResult> GetMovieDetail(Guid id, [FromQuery] Guid cinemaId)
    {
        try
        {
            if (cinemaId == Guid.Empty)
                return BadRequest();

            var movie = await _getMovieDetailUseCase.Run(id, cinemaId);
            return Ok(movie);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("DeleteMovieById")]
    public async Task<IActionResult> DeleteMovie(Guid id, [FromQuery] Guid cinemaId)
    {
        try
        {
            // Em produção, o cinemaId também seria extraído do Token do Admin em vez de QueryString
            if (cinemaId == Guid.Empty)
                return BadRequest();

            await _deleteMovieUseCase.Run(id, cinemaId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
    
}