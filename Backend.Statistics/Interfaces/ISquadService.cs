using DataBase.Statistics.DTOs;

namespace Backend.Statistics.Interfaces;

public interface ISquadService
{
    Task<List<SquadRowDto>> GetSquads();
    
    Task<SquadDto?> GetSquad(string tag);
}