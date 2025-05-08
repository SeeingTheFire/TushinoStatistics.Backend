using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Statistics.Repositories;

public class ReplayRepository(ApplicationContext context) : IReplayRepository
{
    public async Task<List<string>> GetReplayNames()
    {
        return await context.Replays
            .Select(r => r.GameId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Dictionary<string, bool>> CheckIsNewReplays(List<string> newReplays)
    {
         var replaysList = await context.Replays
            .Where(x => newReplays.Contains(x.GameId))
            .Select(r => r.GameId)
            .AsNoTracking()
            .ToListAsync();

         return newReplays.ToDictionary(replay => replay, replay => !replaysList.Contains(replay));
    }

    public void Add(Game gameGame)
    {
        context.Replays.Add(gameGame);
    }
}