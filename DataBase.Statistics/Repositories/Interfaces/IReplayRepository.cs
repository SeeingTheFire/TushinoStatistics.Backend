using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories.Interfaces;

public interface IReplayRepository
{
    Task<List<string>> GetReplayNames();
    void Add(Game gameGame);
}