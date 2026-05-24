using CineSeats.Catalogue.Application.DTOs.Room_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace CineSeats.Catalogue.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IAddRoomUseCase _addRoomUseCase;
    private readonly IUpdateRoomUseCase _updateRoomUseCase;
    private readonly IListRoomsUseCase _listRoomsUseCase;
    private readonly IGetRoomDetailUseCase _getRoomDetailUseCase;
    private readonly IDeleteRoomUseCase _deleteRoomUseCase;
    
    public RoomController(
        IAddRoomUseCase addRoomUseCase,
        IUpdateRoomUseCase updateRoomUseCase,
        IListRoomsUseCase listRoomsUseCase,
        IGetRoomDetailUseCase getRoomDetailUseCase,
        IDeleteRoomUseCase deleteRoomUseCase)
    {
        _addRoomUseCase = addRoomUseCase;
        _updateRoomUseCase = updateRoomUseCase;
        _listRoomsUseCase = listRoomsUseCase;
        _getRoomDetailUseCase = getRoomDetailUseCase;
        _deleteRoomUseCase = deleteRoomUseCase;
    }
    
    [HttpPost("PostRoom")]
    public async Task<IActionResult> AddRoom([FromBody] AddRoomRequest request)
    {
        try
        {
            // Em produção, o CinemaId seria extraído do Token JWT do utilizador autenticado:
            // request.CinemaId = Guid.Parse(User.FindFirst("CinemaId").Value);
            
            await _addRoomUseCase.Run(request);
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

    [HttpPut("UpdateRoom")]
    public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] UpdateRoomRequest request)
    {
        try
        {
            if (id != request.Id)
                return BadRequest(new { error = "O ID da rota difere do ID informado no corpo da requisição." });

            await _updateRoomUseCase.Run(request);
            
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
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro interno no servidor." });
        }
    }

    [HttpGet("GetRooms")]
    public async Task<IActionResult> GetRooms([FromQuery] Guid cinemaId)
    {
        try
        {
            if (cinemaId == Guid.Empty)
                return BadRequest(new { error = "O CinemaId é obrigatório para listar as salas." });

            var rooms = await _listRoomsUseCase.Run(cinemaId);

            if (!rooms.Any())
                return NoContent();
            
            return Ok(rooms);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro interno no servidor." });
        }
    }

    [HttpGet("GetRoomDetail")]
    public async Task<IActionResult> GetRoomDetail(Guid id, [FromQuery] Guid cinemaId)
    {
        try
        {
            if (cinemaId == Guid.Empty)
                return BadRequest(new { error = "O CinemaId é obrigatório." });

            var room = await _getRoomDetailUseCase.Run(id, cinemaId);
            return Ok(room);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro interno no servidor." });
        }
    }

    [HttpDelete("DeleteRoom")]
    public async Task<IActionResult> DeleteRoom(Guid id, [FromQuery] Guid cinemaId)
    {
        try
        {
            if (cinemaId == Guid.Empty)
                return BadRequest(new { error = "O CinemaId é obrigatório." });

            await _deleteRoomUseCase.Run(id, cinemaId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro interno no servidor." });
        }
    }
}