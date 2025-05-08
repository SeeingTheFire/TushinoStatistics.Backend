using Components.Statistics.Services.Interfaces;
using DataBase.Statistics.DTOs;
using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.DomainServices;

public class SquadService(ISquadRepository squadRepository) : ISquadService
{
    public async Task UpdateSquads(List<Squad> replaySquads)
    {
        var tags = replaySquads.Select(s => s.Tag);
        var squads = await squadRepository.GetByTags(tags);
        foreach (var replaySquad in replaySquads)
        {
            if (squads.FirstOrDefault(s => s.Tag == replaySquad.Tag) is {  } squad)
            {
                squad.Update(replaySquad);
            }
            else
            {
                squadRepository.Add(replaySquad);
            }
        }
    }

    public async Task<List<SquadRowDto>> GetList()
    {
       return await squadRepository.GetList();
    }
}