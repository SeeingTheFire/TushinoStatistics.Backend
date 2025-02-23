using System.Linq.Expressions;
using DataBase.Statistics.DTOs;
using Domain.Statistics.Entities;

namespace DataBase.Statistics.Repositories.Interfaces;

public interface ISquadRepository
{
    public Task<IList<Squad>> GetAll(Expression<Func<Squad, bool>>? predicate = null);
    public Task<Squad?> GetByTag(string tag);

    Task<List<Squad>> GetByTags(IEnumerable<string> tags);
    void Add(Squad replaySquad);
    Task<List<SquadRowDto>> GetList();
}