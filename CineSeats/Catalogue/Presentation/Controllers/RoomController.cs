using CineSeats.Catalogue.Application.DTOs.Room_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Room_IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace CineSeats.Catalogue.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IAddRoomUseCase _addRoomUseCase;
    private readonly IGetRoomOrRoomsUseCase _getRoomOrRoomsUseCase;
    private readonly IUpdateRoomUseCase _updateRoomUseCase;
    private readonly IDeleteRoomUseCase _deleteRoomUseCase;
    
    public RoomController(
        IAddRoomUseCase addRoomUseCase,
        IGetRoomOrRoomsUseCase getRoomOrRoomsUseCase,
        IUpdateRoomUseCase updateRoomUseCase,
        IDeleteRoomUseCase deleteRoomUseCase)
    {
        _addRoomUseCase = addRoomUseCase;
        _getRoomOrRoomsUseCase = getRoomOrRoomsUseCase;
        _updateRoomUseCase = updateRoomUseCase;
        _deleteRoomUseCase = deleteRoomUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddRoomRequest request)
    {
        try
        {
            await _addRoomUseCase.Run(request);
            return StatusCode(201);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao cadastrar a sala." });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var rooms = await _getRoomOrRoomsUseCase.Run();

            if (!rooms.Any())
                return NoContent();

            return Ok(rooms);
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao listar as salas." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var room = await _getRoomOrRoomsUseCase.Run(id);
            return Ok(room);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao buscar os detalhes da sala." });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoomRequest request)
    {
        try
        {
            request.Id = id;
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
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao atualizar a sala." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteRoomUseCase.Run(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao remover a sala." });
        }
    }
}