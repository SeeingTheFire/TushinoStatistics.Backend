using System.Linq.Expressions;
using DataBase.Statistics.DTOs;
using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Statistics.Repositories;

public class SquadRepository(ApplicationContext context) : ISquadRepository
{
    public async Task<IList<Squad>> GetAll(Expression<Func<Squad, bool>>? filter = null)
    {
        IQueryable<Squad> query = context.Squads;
        if (filter != null)
            query = query.Where(filter);
        
        return await query.ToListAsync();
    }

    public Task<Squad?> GetByTag(string tag)
    {
        var squad = context.Squads
            .Include(x => x.Players)
            .ThenInclude(x => x.Kills)
            .Include(x => x.Players)
            .ThenInclude(x => x.Deaths)
            .Include(x => x.Players)
            .ThenInclude(x => x.Attendances)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefault(x => x.Tag == tag);

        return Task.FromResult(squad);

    }

    public async Task<List<Squad>> GetByTags(IEnumerable<string> tags)
    {
        return await context.Squads
            .Where(x => tags.Contains(x.Tag))
            .AsNoTracking()
            .ToListAsync();
    }

    public void Add(Squad squad)
    {
        context.Squads.Add(squad);
    }

    public async Task<List<SquadRowDto>> GetList()
    {
        return context.Squads
            .Include(x=>x.Players)
            .ThenInclude(x=>x.Kills)
            .Include(x=>x.Players)
            .ThenInclude(x=>x.Attendances)
            .AsNoTracking()
            .Select(x=>new SquadRowDto
        {
            Tag = x.Tag,
            KillsCount = x.Players.Sum(p=>p.Kills.Count),
            AttendeesCount = x.Players.Sum(p=>p.Attendances.Count),
            TeamKillsCount = x.Players.Sum(p=>p.Kills.Count(kill=>kill.TeamKill)),
            SurviveCount = x.Players.Sum(p=>p.Attendances.Count(attendance=>!attendance.IsDead)),
            VehicleKillCount = x.Players.Sum(p=>p.Kills.Count(kill=>kill.IsVehicleKilled)),
            InfantryKillCount = x.Players.Sum(p=>p.Kills.Count(kill=>!kill.IsVehicleKilled)),
            Efficiency = 50
        }).ToList();
    }
}