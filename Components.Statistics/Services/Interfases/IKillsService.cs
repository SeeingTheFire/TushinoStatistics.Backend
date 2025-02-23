using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfases;

public interface IKillsService
{
    Task AddKills(List<Kill> kills);
}