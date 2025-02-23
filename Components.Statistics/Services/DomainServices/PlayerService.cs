using Components.Statistics.Services.Interfases;
using DataBase.Statistics.Models.DataTransferObjects;
using DataBase.Statistics.Repositories.Interfaces;
using Domain.Statistics.Entities;

namespace Components.Statistics.Services.DomainServices;

public class PlayerService(IPlayerRepository playerRepository) : IPlayerService
{
    public async Task UpdatePlayers(List<Player> replayPlayers, Game gameGame)
    {
        var tags = replayPlayers.Select(s => s.SteamId);
        var players = await playerRepository.GetByIds(tags);
        foreach (var replayPlayer in replayPlayers)
        {
            if (players.FirstOrDefault(s => s.SteamId == replayPlayer.SteamId) is {  } player)
            {
                if (player.Tag != replayPlayer.Tag || player.Name != replayPlayer.Name) 
                {
                    player.Update(replayPlayer);
                }
                
                player.Games.Add(gameGame);
            }
            else
            {
                replayPlayer.Games.Add(gameGame);
                playerRepository.Add(replayPlayer);
            }
        }
    }

    public async Task<List<PlayerDto>> GetList()
    {
        return await playerRepository.GetAllList();
    }
    
    /// <inheritdoc />
    public async Task<PlayerDto?> GetPlayer(string name, DateTime limitedDateTime)
    {
        return await playerRepository.GetByName(name, limitedDateTime);
    }
}