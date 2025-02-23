using Backend.Statistics.Interfaces;
using DataBase.Statistics.DTOs;
using DataBase.Statistics.Repositories.Interfaces;

namespace Backend.Statistics.Services;

public class SquadService(ISquadRepository repository, ILogger<SquadService> logger) : ISquadService
{
    public async Task<List<SquadRowDto>> GetSquads()
    {
       var squads = await repository.GetAll();
       return squads.Select(x=>new SquadRowDto(x)).ToList();
    }

    public async Task<SquadDto?> GetSquad(string tag)
    {
        var squad = await repository.GetByTag(tag);
        
        
        return squad is not null ?  new SquadDto
        {
            Players = squad.Players.Select(u=> new PlayerRowDto
                (
                    u.Name,
                    u.Tag,
                    u.Kills.Count,
                    u.Attendances.Count(and => and.IsDead),
                    u.Attendances.Count(and => and.IsDead),
                    u.Kills.Count(k => k.TeamKill),
                    u.Attendances.Count(and => !and.IsDead), 
                    u.TreatmentFromPlayer.Count,
                    u.Deaths.Count(k=>k.TeamKill),
                    DateTime.Now)
                    
            ).ToList()
        }: null;
    }
}