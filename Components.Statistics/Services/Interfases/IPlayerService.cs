using DataBase.Statistics.Models.DataTransferObjects;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfases;

public interface IPlayerService
{
    Task UpdatePlayers(List<Player> replayPlayers, Game gameGame);
    Task<List<PlayerDto>> GetList();

    /// <summary>
    /// Возвращает данные об игроке по имени
    /// </summary>
    /// <param name="name">Имя игрока</param>
    /// <param name="limitedDateTime">Ограничение по дате, за которую ищем информацию об игроке</param>
    /// <returns></returns>
    Task<PlayerDto?> GetPlayer(string name, DateTime limitedDateTime);
}