using Components.Statistics.Services.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Statistics.Controllers;


[ApiController]
[Route("squads")]
public class SquadListController(ILogger<PlayerListController> logger, ISquadService squadService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetListSquads()
    {
        return Ok(squadService.GetList());
    }
}