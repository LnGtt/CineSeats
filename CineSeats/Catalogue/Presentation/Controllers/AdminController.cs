using CineSeats.Catalogue.Application.DTOs.Admin_DTOs;
using CineSeats.Catalogue.Application.IUseCases.Admin_IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace CineSeats.Catalogue.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAddAdminUseCase _addAdminUseCase;
    private readonly IUpdateAdminUseCase _updateAdminUseCase;
    
    public AdminController(IAddAdminUseCase addAdminUseCase, IUpdateAdminUseCase updateAdminUseCase)
    {
        _addAdminUseCase = addAdminUseCase;
        _updateAdminUseCase = updateAdminUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAdmin([FromBody] AddAdminRequest request)
    {
        try
        {
            await _addAdminUseCase.Run(request);
            return StatusCode(201);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Erro interno no servidor ao criar o administrador." });
        }
    }

    [HttpPut("{id}/name")]
    public async Task<IActionResult> UpdateName(Guid id, [FromBody] UpdateAdminNameRequest request)
    {
        try
        {
            request.Id = id;
            await _updateAdminUseCase.Run(request);
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
            return StatusCode(500, new { error = "Erro interno no servidor ao atualizar o nome." });
        }
    }

    [HttpPut("{id}/email")]
    public async Task<IActionResult> UpdateEmail(Guid id, [FromBody] UpdateAdminEmailRequest request)
    {
        try
        {
            request.Id = id;
            await _updateAdminUseCase.Run(request);
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
            return StatusCode(500, new { error = "Erro interno no servidor ao atualizar o e-mail." });
        }
    }

    [HttpPut("{id}/password")]
    public async Task<IActionResult> UpdatePassword(Guid id, [FromBody] UpdateAdminPasswordRequest request)
    {
        try
        {
            request.Id = id;
            await _updateAdminUseCase.Run(request);
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
            return StatusCode(500, new { error = "Erro interno no servidor ao atualizar a senha." });
        }
    }
    
    //arrumar depois
    [HttpPost("login")] //improvisado, esquecemos dos métodos de login
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Bypass
        return Ok(new { success = true, message = "Login liberado" });
    }
}
//DEPOIS ARRUMAR
public class LoginRequest //improvisado
{
    public string Email { get; set; }
    public string Password { get; set; }
}