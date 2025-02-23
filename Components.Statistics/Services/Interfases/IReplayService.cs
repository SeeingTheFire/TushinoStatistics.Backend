using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfases;

public interface IReplayService
{
    Task<List<string>> GetReplayNames();
    void AddNewReplay(Game gameGame);
}