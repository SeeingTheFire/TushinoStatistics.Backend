using Domain.Statistics.Entities;

namespace DataBase.Statistics.DTOs;

public class SquadRowDto
{
    public SquadRowDto()
    {
        
    }
    public SquadRowDto(Squad squad)
    {
        Tag = squad.Tag;
        KillsCount = squad.Players.Sum(x => x.Kills.Count);
        TeamKillsCount = squad.Players.Sum(x => x.Kills.Count(k => k.TeamKill));
        SurviveCount = squad.Players.Sum(x => x.Attendances.Count - x.Deaths.Count);
        AttendeesCount = squad.Players.Sum(x => x.Attendances.Count);
        VehicleKillCount = squad.Players.Sum(x => x.Kills.Count);
        InfantryKillCount = squad.Players.Sum(x => x.Kills.Count);
        Efficiency = AttendeesCount > 0 ? (KillsCount - TeamKillsCount) / AttendeesCount : 0;
    }

    public string Tag { get; set; }
    public int KillsCount { get; set; }
    public int TeamKillsCount { get; set; }
    public int SurviveCount { get; set; }
    public int AttendeesCount { get; set; }
    public int VehicleKillCount { get; set; }
    public int InfantryKillCount { get; set; }
    public double Efficiency { get; set; }
}