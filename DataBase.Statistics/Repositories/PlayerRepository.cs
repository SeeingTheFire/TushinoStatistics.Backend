using DataBase.Statistics.Models.DataTransferObjects;
using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Statistics.Repositories;

public class PlayerRepository(ApplicationContext context) : IPlayerRepository
{
    public async Task<List<PlayerDto>> GetAllList()
    {
        var list = await context.Players
            .AsNoTracking()
            .Include(x => x.Kills)
            .Include(x => x.Deaths)
            .Include(x => x.Attendances)
            .Include(x => x.TreatmentFromPlayer)
            .AsSplitQuery()
            .AsNoTracking()
            .Select(x => new PlayerDto(x))
            .ToListAsync();

        return list;
    }

    /// <inheritdoc />
    public async Task<PlayerDto?> GetById(long steamId)
    {
        // Получим данные об игроке из контекста
        var player = await context.Players
            .Include(player => player.Kills)
            .Include(player => player.Deaths)
            .Include(x => x.Attendances)
            .Include(x => x.TreatmentFromPlayer)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SteamId == steamId);
        
        if (player is null)
        {
            return null;
        }
        
        return new PlayerDto(player);
    }

    /// <inheritdoc />
    public async Task<PlayerDto?> GetByName(string name, DateTime limitedDateTime)
    {
        var player = await context.Players
            .Include(player => player.Kills.Where(kill => kill.Date <= limitedDateTime))
            .Include(player => player.Deaths.Where(death => death.Date <= limitedDateTime))
            .Include(x => x.Attendances.Where(attendance => attendance.Date <= limitedDateTime))
            .Include(x => x.TreatmentFromPlayer.Where(medicalInfo => medicalInfo.Date <= limitedDateTime))
            .Include(x => x.Games.Where(game => game.Date <= limitedDateTime))
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
        
        if (player is null)
        {
            return null;
        }

        return new PlayerDto(player);
    }

    public Task<List<Player>> GetByIds(IEnumerable<long> tags)
    {
        return context.Players
            .Where(x => tags.Contains(x.SteamId))
            .Include(x => x.Games)
            .AsSplitQuery()
            .ToListAsync();
    }

    public void Add(Player replayPlayer)
    {
        context.Players.Add(replayPlayer);
    }
}