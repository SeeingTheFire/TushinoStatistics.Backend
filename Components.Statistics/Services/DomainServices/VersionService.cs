using Components.Statistics.Services.Interfases;
using DataBase.Statistics.Repositories.Interfaces;

namespace Components.Statistics.Services.DomainServices;

public class VersionService(IVersionRepository versionRepository) : IVersionService
{
    public void UpdateVersion()
    {
        versionRepository.Add(new Domain.Statistics.Entities.Version(DateTime.UtcNow));
    }
}