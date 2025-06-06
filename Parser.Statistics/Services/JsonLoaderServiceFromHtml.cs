﻿using System.Text.Json;
using DataBase.Statistics.Models;
using Domain.Statistics.Entities;
using Parser.Statistics.Resource;
using Parser.Statistics.Services.Interfaces;

namespace Parser.Statistics.Services;

public class JsonLoaderServiceFromHtml(IHttpClientFactory client) : IJsonLoaderService
{
    private readonly HttpClient _client = client.CreateClient("Parser");

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    }; 

    // Ссылки на реплеи
    private const string UrlSg = "https://game.tsgames.ru/ajax.php?a=l&params%5Bf%5D%5B%5D=2&params%5Bf%5D%5B%5D=3&params%5Bf%5D%5B%5D=4&params%5Bf%5D%5B%5D=10&params%5Bf%5D%5B%5D=20%3A1";
    //private const string UrlMace = "https://game.tsgames.ru/ajax.php?a=l&params%5Bf%5D%5B%5D=1&params%5Bf%5D%5B%5D=11&params%5Bf%5D%5B%5D=20%3A1";

    /// <summary>
    /// Получение имен реплеев с адресов
    /// </summary>
    /// <param name="client">Клиент Http</param>
    /// <param name="uri">Uri с реплеями</param>
    /// <returns></returns>
    private static async Task<Stream?> GetReplayNames(HttpClient client, string uri)
    {
        try
        {
            var response = await client.GetStreamAsync(uri);
            return response;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(Resources.Cant_connect_to_replay_server);
            return null;
        }
    }

    /// <summary>
    /// Метод получения новых строк
    /// </summary>
    /// <param name="replays"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    private async Task<List<Row>> GetNewRows(List<string> replays, string uri)
    {
        // Получаем Json список всех игр
        var gamesListJson = await GetReplayNames(_client, uri);
        if (gamesListJson is null)
        {
            return [];
        }

        // Десереализуем его в строки
        try
        {
            var replayCollectionFromJson = await JsonSerializer.DeserializeAsync<JsonReplayRowDto>(gamesListJson, Options);
            // Сверяем те реплеии которых нет в бд
            return replayCollectionFromJson == null
                ? []
                : replayCollectionFromJson.Rows.Where(row => !replays.Contains(row.Name)).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    /// <summary>
    /// <inheritdoc cref="IJsonLoaderService.GetJsonReplays"/>
    /// </summary>
    /// <returns></returns>
    public async Task<List<Game>> GetJsonReplays(List<string> replays)
    {
        var newReplays = await GetNewRows(replays, UrlSg);
        var replaysToBd = new List<Game>();

        // Переводим их в объект реплей
        foreach (var row in newReplays.DistinctBy(x=>x.Name))
        {
            // Переделываем дата и время под тип значения нужный для бд
            if (DateTime.TryParse(row.Array[1], out var date))
            {
                replaysToBd.Add(new Game
                {
                    Name = row.Array[2],
                    Date = date.ToUniversalTime(),
                    GameId = row.Name,
                    Map = row.Array[3],
                    Server = row.Array[0]
                });
            }
        }

        return replaysToBd;
    }

    /// <summary>
    /// Получение информации с 1 реплея
    /// </summary>
    /// <param name="name">Имя реплея</param>
    /// <returns></returns>
    public async Task<List<List<object>>?> GetReplayInformation(string name)
    {
        var uri = $"https://game.tsgames.ru/ajax.php?a=gl&params%5Bf%5D={name}&params%5Bar%5D=1&params%5Ba%5D=3";
        var response = await _client.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(Resources.ResultUri, uri);
        
        var responseString = await response.Content.ReadAsStringAsync();
        var inf = JsonSerializer.Deserialize<ReplayInf>(responseString, Options);

        return inf is null ? null :
            // Получение объекта ReplayInformation
            JsonSerializer.Deserialize<List<List<object>>>(inf.Json);
    }
}