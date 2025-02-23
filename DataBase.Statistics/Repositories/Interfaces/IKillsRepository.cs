using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories.Interfaces;

public interface IKillsRepository
{
    void AddRange(List<Kill> kills);
}