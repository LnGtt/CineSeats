using CineSeats.Tickets.Application.IUseCases.SessionSeat_IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace CineSeats.Tickets.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionSeatsController : ControllerBase
{
    private readonly IGetSessionSeatsUseCase _getSessionSeatsUseCase;

    public SessionSeatsController(IGetSessionSeatsUseCase getSessionSeatsUseCase)
    {
        _getSessionSeatsUseCase = getSessionSeatsUseCase;
    }

    [HttpGet("{sessionId}")]
    public async Task<IActionResult> GetSeats(Guid sessionId)
    {
        try
        {
            var seats = await _getSessionSeatsUseCase.Run(sessionId);
            
            if (!seats.Any())
                return NoContent();
                
            return Ok(seats);
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno ao carregar o mapa de cadeiras." });
        }
    }
}