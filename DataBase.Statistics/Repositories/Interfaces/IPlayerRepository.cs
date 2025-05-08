using System.Linq.Expressions;
using DataBase.Statistics.Models.DataTransferObjects;
using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories.Interfaces;

public interface IPlayerRepository
{
    /// <summary>
    /// Получение листа пользователей
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task<List<PlayerDto>> GetAllList(Expression<Func<Player, bool>>? filter = null);
    
    /// <summary>
    /// Возвращает информацию об игроке по идентификатору Steam
    /// </summary>
    /// <param name="steamId">Steam идентификатор</param>
    /// <returns></returns>
    public Task<PlayerDto?> GetById(long steamId);

    /// <summary>
    /// Возвращает информацию об игроке по имени
    /// </summary>
    /// <param name="name">Имя игрока</param>
    /// <param name="limitedDateTime">limitedDateTime</param>
    /// <returns></returns>
    public Task<PlayerDto?> GetByName(string name, DateTime limitedDateTime);
    
    Task<List<Player>> GetByIds(IEnumerable<long> tags);
    void Add(Player replayPlayer);
}