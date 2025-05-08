using Components.Statistics.Services.Interfaces;
using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.DomainServices;

public class ReplayService(IReplayRepository replayRepository) : IReplayService
{
    public async Task<List<string>> GetReplayNames()
    {
        return await replayRepository.GetReplayNames();
    }

    public void AddNewReplay(Game gameGame)
    {
        replayRepository.Add(gameGame);
    }
}