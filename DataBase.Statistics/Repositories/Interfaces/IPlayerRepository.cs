using DataBase.Statistics.Models.DataTransferObjects;
using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories.Interfaces;

public interface IPlayerRepository
{
    public Task<List<PlayerDto>> GetAllList();
    
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