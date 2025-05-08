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
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };
    
    [HttpGet("{tag}")]
    [ProducesResponseType(typeof(SquadDto), 200)]
    [ProducesResponseType( 404)]
    public IActionResult GetSquads(string tag)
    {
        logger.LogInformation("Запрос отряда из БД");
        return StatusCode(200, JsonSerializer.Serialize(service.GetSquad(tag), JsonSerializerOptions));
    }
}