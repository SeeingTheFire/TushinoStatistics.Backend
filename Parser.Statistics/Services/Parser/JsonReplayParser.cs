using DataBase.Statistics.Models;
using Domain.Statistics.Entities;
using Parser.Statistics.Services.Interfaces;

namespace Parser.Statistics.Services.Parser;

/// <summary>
/// Парсер реплея
/// </summary>
public class JsonReplayParser : IJsonReplayParser
{
    /// <summary>
    /// Получение информации в базу данных из реплея
    /// </summary>
    /// <param name="replay">Объект реплей</param>
    /// <param name="gameTdo">Идентификатора игры (реплея)</param>
    /// <param name="cancellationToken"></param>
    public Task<ReplayInfo?> GetInformationFromJson(List<List<object>> replay, Game gameTdo, CancellationToken cancellationToken)
    {
        // Проверяем что json реплей не пустой
        if (replay.Count < 2 || replay[1].Count < 2 || cancellationToken.IsCancellationRequested)
        {
            return Task.FromResult<ReplayInfo?>(null);
        }
        
        // Создаем объект информации из реплея
        var replayJson = new ReplayInfo(gameTdo);

        // Получаем информации о ботах, игроках и технике
        if (replayJson.WriteBotInformation(replay) || cancellationToken.IsCancellationRequested)
        {
            return Task.FromResult<ReplayInfo?>(null);
        }

        // Получаем информацию об убийствах
        replayJson.WriteEventsInformation(replay, gameTdo);
        return Task.FromResult(replayJson)!;
    }
}