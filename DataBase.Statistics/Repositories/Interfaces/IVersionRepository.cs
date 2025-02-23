using Version = Domain.Statistics.Entities.Version;

namespace DataBase.Statistics.Repositories.Interfaces;

public interface IVersionRepository
{
    void Add(Version version);
}