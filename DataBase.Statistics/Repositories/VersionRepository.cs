using DataBase.Statistics.Repositories.Interfaces;
using Version = Domain.Statistics.Entities.Version;

namespace DataBase.Statistics.Repositories;

public class VersionRepository(ApplicationContext context) : IVersionRepository
{
    public void Add(Version version)
    {
        context.Add(version);
    }
}