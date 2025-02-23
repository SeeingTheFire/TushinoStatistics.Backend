using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.Statistics.Interfaces;
using DataBase.Statistics.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Statistics.Controllers;

[ApiController]
[Route("squad")]
public class SquadController(ILogger<SquadController> logger, ISquadService service) : ControllerBase
{
    
    [HttpGet("{tag}")]
    [ProducesResponseType(typeof(SquadDto), 200)]
    [ProducesResponseType( 404)]
    public IActionResult GetSquads(string tag)
    {
        logger.LogInformation("Запрос отряда из БД");
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        
        return StatusCode(200, JsonSerializer.Serialize(service.GetSquad(tag), options));
    }
}