using DataBase.Statistics.DTOs;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.Interfaces;

public interface ISquadService
{
    Task UpdateSquads(List<Squad> replaySquads);
    Task<List<SquadRowDto>> GetList();
}