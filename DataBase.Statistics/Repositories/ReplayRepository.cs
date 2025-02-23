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

    public void Add(Game gameGame)
    {
        context.Replays.Add(gameGame);
    }
}