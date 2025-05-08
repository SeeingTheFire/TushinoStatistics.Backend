using DataBase.Statistics.Models.DataTransferObjects;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfaces;

public interface IPlayerService
{
    /// <summary>
    /// Обновление статистики
    /// </summary>
    /// <param name="replayPlayers"></param>
    /// <param name="gameGame"></param>
    /// <returns></returns>
    Task UpdatePlayers(List<Player> replayPlayers, Game gameGame);
    
    /// <summary>
    /// Получение листа пользователей
    /// </summary>
    /// <returns></returns>
    Task<List<PlayerDto>> GetList(DateOnly? dateOnly = null);

    /// <summary>
    /// Возвращает данные об игроке по имени
    /// </summary>
    /// <param name="name">Имя игрока</param>
    /// <param name="limitedDateTime">Ограничение по дате, за которую ищем информацию об игроке</param>
    /// <returns></returns>
    Task<PlayerDto?> GetPlayer(string name, DateTime limitedDateTime);
}