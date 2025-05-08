using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfaces;

public interface IReplayService
{
    Task<List<string>> GetReplayNames();
    void AddNewReplay(Game gameGame);
}