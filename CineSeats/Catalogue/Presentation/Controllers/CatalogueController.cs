using Microsoft.AspNetCore.Mvc;

namespace CineSeats.Catalogue.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogueController : ControllerBase
{
    //PARA FACILITAR A CODAGEM, ESTA CLASSE SERÁ CODADA EM FORMATO DE MONÓLITO
    //APÓS ISSO, APLICAREMOS PRINCIPIOS DE ARQUITETURA E DDD

    public CatalogueController(/*use cases*/)
    {
        throw new NotImplementedException("Not implemented yet");
    }

    [HttpGet("GetCatalogue")]
    public IActionResult GetCatalogue()
    {
        try
        {
            var movies = _useCase1.GetAllMovies();

            if (movies = null || !movies.Any())
            {
                return NoContent();
            }
            
            return Ok(movies);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
        }
    }

    [HttpGet("GetMovie")]
    public IActionResult GetMovie([FromQuery] Guid id)
    {
        try
    }
    
}