using DataBase.Statistics.Models;
using Domain.Statistics.Entities;

namespace Parser.Statistics.Services.Interfaces;

/// <summary>
/// Парсер Json реплеев
/// </summary>
public interface IJsonReplayParser
{
    // Получение информации для добавления базы данных
    public Task<ReplayInfo?> GetInformationFromJson(List<List<object>> replayRepl, Game gameTdo, CancellationToken cancellationToken);
}