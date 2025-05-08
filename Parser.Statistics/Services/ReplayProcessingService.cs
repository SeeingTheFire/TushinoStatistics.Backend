using Components.Statistics.Services.Interfaces;
using DataBase.Statistics;
using DataBase.Statistics.Models;
using Domain.Statistics.Entities;
using Parser.Statistics.Services.Interfaces;

namespace Parser.Statistics.Services;

/// <summary>
/// Сервис по обработке реплеев
/// </summary>
public class ReplayProcessingService(ILogger<ReplayProcessingService> logger, IReplayService replayService,
    IPlayerService playerService, ISquadService squadService, 
    IKillsService killsService, IAttendancesService attendancesService, 
    IVehicleService vehicleService, IMedicalService medicalService, 
    IJsonLoaderService loaderService, IVersionService versionService, 
    IJsonReplayParser parser, IUnitOfWorkFactory unitOfWorkFactory) : IReplayProcessingService
{
    /// <inheritdoc/>
    public async Task UpdateReplaysCollection(CancellationToken cancellationToken)
    {
        // Получаем реплеи из бд
        var replaysInBd = await replayService.GetReplayNames();

        // Получаем новые реплеи которых нет в бд
        var newReplays = await loaderService.GetJsonReplays(replaysInBd);
        if (newReplays.Count == 0)
        {
            logger.LogInformation("Новых реплеев не найдено");
            return;
        }

        if (cancellationToken.IsCancellationRequested)
            return;
        
        //TODO: Временный лок количества реплеев
        var splittedReplays = newReplays//.Where(x=>x.GameId == "T3.2024-01-06-23-27-20.TSG@216_fra_PF_Day_Eight_v7.fallujahint")
            .Take(1)
            .ToList();
        
        
        // Получаем таски для каждого реплея по получению информации с реплея
        var tasks = splittedReplays
            .Select(async repl => await GetReplayInformation(repl, cancellationToken));

        if (cancellationToken.IsCancellationRequested)
            return;
        
        // Ожидаем выполнение всех тасков
        var replayInfos = await Task.WhenAll(tasks);
        if (replayInfos.Length > 0)
        {
            foreach (var replay in replayInfos.OfType<ReplayInfo>())
            {
                await UpdateStatistics(replay);
            }
        }
    }

    private async Task UpdateStatistics(ReplayInfo replay)
    {
        var unitOfWork = unitOfWorkFactory.Create();
        replayService.AddNewReplay(replay.Game);
        await squadService.UpdateSquads(replay.Squads.Values.ToList());
        
        var kills = replay.Kills.ToList();
        var killsDictionary = new Dictionary<long, Kill>();
        foreach (var kill in kills)
        {
            if (kill.KilledSteamId.HasValue && !killsDictionary.TryGetValue(kill.KilledSteamId.Value, out _))
            {
                killsDictionary.Add(kill.KilledSteamId.Value, kill);
            }
        }
        
        var damageKilledDamage = replay.DamageInfos
            .GroupBy(x=>x.KilledSteamId, x=>x)
            .ToDictionary(x=>x.Key, x=>x.ToList());
        
        var murderDamage = replay.DamageInfos
            .GroupBy(x=>x.MurdererSteamId, x=>x)
            .Where(x => x.Key is not null)
            .ToDictionary(x=>x.Key, x=>x.ToList());

        var attendances = replay.Players
            .Select(player => new Attendance(replay.Game, player.Value.SteamId, 
                killsDictionary.TryGetValue(player.Value.SteamId, out _),
                murderDamage.TryGetValue(player.Value.SteamId, out var damages) ? damages : []))
            .ToList();
        

        var players = replay.Players.Values.ToList();
        await playerService.UpdatePlayers(players, replay.Game);
        await killsService.AddKills(kills);
        await attendancesService.AddAttendances(attendances);
        await vehicleService.UpdateVehicles(replay.Vehicles.Values.ToList());
        await medicalService.AddMedicalInfos(replay.MedicalsInfo.ToList());

        versionService.UpdateVersion();
        await unitOfWork.SaveChangesAsync();

        unitOfWork.Complete();
    }
    
    /// <summary>
    /// Получение информации из реплея
    /// </summary>
    /// <param name="gameTdo">Реплей ДТО</param>
    /// <param name="cancellationToken"></param>
    private async Task<ReplayInfo?> GetReplayInformation(Game gameTdo, CancellationToken cancellationToken)
    {
        // Грузим json с сервера
        var replay = await loaderService.GetReplayInformation(gameTdo.GameId);
        if (replay is null)
        {
            return null;
        }

        if (cancellationToken.IsCancellationRequested)
            return null;

        // Парсим информацию из json
        return await parser.GetInformationFromJson(replay, gameTdo, cancellationToken);
    }
}