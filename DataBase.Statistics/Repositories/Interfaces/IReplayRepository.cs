using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories.Interfaces;

public interface IReplayRepository
{
    Task<List<string>> GetReplayNames();
    Task<Dictionary<string, bool>> CheckIsNewReplays(List<string> newReplays);
    void Add(Game gameGame);
}