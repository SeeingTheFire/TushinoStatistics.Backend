using Components.Statistics.Services.Interfases;
using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.DomainServices;

public class KillsService(IKillsRepository killsRepository) : IKillsService
{
    public Task AddKills(List<Kill> kills)
    {
        killsRepository.AddRange(kills);
        return Task.CompletedTask;
    }
}