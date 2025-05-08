using Components.Statistics.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Statistics.Controllers;

/// <summary>
/// Контроллер данных об игроке
/// </summary>
[ApiController]
[Route("player")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerListController> _logger;
    private readonly IPlayerService _playerService;
    private readonly IConfiguration _configuration;
    
    private readonly DateTime _defaultLimitedDateTime = new DateTime(2020, 1, 1);

    /// <inheritdoc />
    public PlayerController(ILogger<PlayerListController> logger, IPlayerService playerService, IConfiguration configuration)
    {
        _logger = logger;
        _playerService = playerService;
        _configuration = configuration;
    }

    /// <summary>
    /// Возвращает информацию об игроке
    /// </summary>
    /// <param name="name">Имя игрока</param>
    /// <returns></returns>
    [HttpGet("player/{name}")]
    public async Task<IActionResult> GetPlayer(string name)
    {
        if (!DateTime.TryParse(_configuration["LimitedDateTime"], out var limitedDateTime) 
            || limitedDateTime < _defaultLimitedDateTime)
        {
            limitedDateTime = _defaultLimitedDateTime;
        }
        
        if (await _playerService.GetPlayer(name, limitedDateTime.ToUniversalTime()) is not { } player )
        {
            return BadRequest("Не удалось найти информацию об игроке");
        }
        
        return Ok(player);
    }
}