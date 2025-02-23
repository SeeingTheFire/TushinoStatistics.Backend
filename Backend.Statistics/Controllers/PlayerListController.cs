using Components.Statistics.Services.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Statistics.Controllers;

[ApiController]
[Route("players")]
public class PlayerListController(ILogger<PlayerListController> logger, IPlayerService playerService, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public IActionResult GetListUsers()
    {
        return Ok(playerService.GetList());
    }
}