using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfaces;

public interface IKillsService
{
    Task AddKills(List<Kill> kills);
}