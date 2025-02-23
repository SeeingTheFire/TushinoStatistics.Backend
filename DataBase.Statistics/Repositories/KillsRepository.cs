using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories;

public class KillsRepository(ApplicationContext context) : IKillsRepository
{
    public void AddRange(List<Kill> kills)
    {
        context.Kills.AddRange(kills);
    }
}