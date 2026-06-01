using CineSeats.Catalogue.Application.DTOs.Session_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Session_IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace CineSeats.Catalogue.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private readonly IAddSessionUseCase _addSessionUseCase;
    private readonly IGetSessionOrSessionsUseCase _getSessionOrSessionsUseCase;
    private readonly IUpdateSessionUseCase _updateSessionUseCase;
    private readonly IDeleteSessionUseCase _deleteSessionUseCase;

    public SessionController(
        IAddSessionUseCase addSessionUseCase,
        IGetSessionOrSessionsUseCase getSessionOrSessionsUseCase,
        IUpdateSessionUseCase updateSessionUseCase,
        IDeleteSessionUseCase deleteSessionUseCase)
    {
        _addSessionUseCase = addSessionUseCase;
        _getSessionOrSessionsUseCase = getSessionOrSessionsUseCase;
        _updateSessionUseCase = updateSessionUseCase;
        _deleteSessionUseCase = deleteSessionUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddSessionRequest request)
    {
        try
        {
            await _addSessionUseCase.Run(request);
            return StatusCode(201);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao criar a sessão." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var session = await _getSessionOrSessionsUseCase.GetSessionById(id);
            return Ok(session);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao buscar a sessão." });
        }
    }

    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetByMovieId(Guid movieId)
    {
        try
        {
            var sessions = await _getSessionOrSessionsUseCase.GetSessionsByMovieId(movieId);
            
            if (!sessions.Any())
                return NoContent();

            return Ok(sessions);
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao buscar sessões do filme." });
        }
    }

    [HttpGet("room/{roomId}")]
    public async Task<IActionResult> GetByRoomId(Guid roomId)
    {
        try
        {
            var sessions = await _getSessionOrSessionsUseCase.GetSessionsByRoomId(roomId);
            
            if (!sessions.Any())
                return NoContent();

            return Ok(sessions);
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao buscar sessões da sala." });
        }
    }

    [HttpPut("{id}/details")]
    public async Task<IActionResult> UpdateDetails(Guid id, [FromBody] UpdateSessionDetailsRequest request)
    {
        try
        {
            request.Id = id;
            await _updateSessionUseCase.RunDetails(request);
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
            return StatusCode(500, new { error = "Erro interno no servidor ao atualizar os detalhes da sessão." });
        }
    }

    [HttpPut("{id}/room")]
    public async Task<IActionResult> ChangeRoom(Guid id, [FromBody] UpdateSessionRoomRequest request)
    {
        try
        {
            request.Id = id;
            await _updateSessionUseCase.RunRooms(request);
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
            return StatusCode(500, new { error = "Erro interno no servidor ao alterar a sala da sessão." });
        }
    }

    [HttpPut("{id}/start-time")]
    public async Task<IActionResult> UpdateStartTime(Guid id, [FromBody] UpdateSessionStartTimeRequest request)
    {
        try
        {
            request.Id = id;
            await _updateSessionUseCase.RunStartTime(request);
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
            return StatusCode(500, new { error = "Erro interno no servidor ao atualizar o horário da sessão." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteSessionUseCase.Run(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao remover a sessão." });
        }
    }
}