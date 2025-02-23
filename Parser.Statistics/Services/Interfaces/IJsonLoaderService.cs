using DataBase.Statistics.Models;
using Domain.Statistics.Entities;

namespace Parser.Statistics.Services.Interfaces;

public interface IJsonLoaderService
{
    /// <summary>
    /// Получение коллекции реплеев в формате JSON
    /// </summary>
    /// <returns>Коллекция реплеев</returns>
    public Task<List<Game>> GetJsonReplays(List<string> replays);


    /// <summary>
    /// Получение информации с 1 реплея
    /// </summary>
    /// <param name="uri">Ссылка</param>
    /// <returns></returns>
    public Task<ReplayInformation?> GetReplayInformation(string uri);
}