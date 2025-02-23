using System.Text.Json;
using System.Text.RegularExpressions;
using DataBase.Statistics.Models;
using Domain.Statistics.Entities;

namespace Parser.Statistics.Services;

public static partial class JsonReplayParserExtensions
{
    /// <summary>
    /// Получение информации о событиях произошедших на карте
    /// </summary>
    /// <param name="replay"></param>
    /// <param name="game"></param>
    /// <param name="replayInfo"></param>
    public static void WriteEventsInformation(this ReplayInfo replayInfo, IEnumerable<List<object>> replay, Game game)
    {
        // Поиск информации в каждом тайминге в игре, пропускаем 2 элемента потому что в 1 элементе все о карте что играется,
        // во втором информация о ботах, и только потом начинается тикрейт реплея
        foreach (var oneTime in replay.Skip(2))
        {
            if (oneTime[1] is not JsonElement { ValueKind: JsonValueKind.Array } element || element.GetArrayLength() == 0)
            {
                continue;
            }

            // Из каждого тайминга вытаскиваем информацию по реплею
            foreach (var replayEvent in element.EnumerateArray())
            {
                var eventId = replayEvent[0].GetInt32();
                switch (eventId)
                {
                    // Ивент отвечающий за событие убийства
                    case 4:
                    {
                        replayInfo.GetKillsInfo(replayEvent, game, oneTime);
                        break;
                    }

                    // Ивент отвечающий за событие нанесения урона без убийства
                    case 5:
                    {
                        replayInfo.GetDamageInfo(replayEvent, game, oneTime);
                        break;
                    }

                    // Ивент отвечающий за событие лечения
                    case 7:
                    {
                        replayInfo.GetMedicalInfo(replayEvent, game, oneTime);
                        break;
                    }

                    // Не знаю пока что за событие мб смерть?
                    case 3:
                    {
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Получение информации об игроках, технике и ботах
    /// </summary>
    /// <param name="replay"></param>
    /// <param name="replayInfo"></param>
    /// <returns></returns>
    public static bool WriteBotInformation(this ReplayInfo replayInfo, IReadOnlyList<List<object>> replay)
    {
        // Список игроков в Json
        if (replay[1][1] is not JsonElement players)
        {
            return true;
        }

        // Добавляем информацию о играках
        foreach (var row in players.EnumerateArray())
        {
            switch (row[0].GetInt32())
            {
                // Добавления коллекции бота
                case 1:
                {
                    var botId = row[1].GetInt32();
                    var side = row[4].GetInt32();

                    replayInfo.AddBot(botId, side);
                    break;
                }

                // Добавление техники используемой на миссии
                case 2:
                {
                    var vehicleId = row[1].GetInt32();
                    var name = row[2].GetString();
                    var classVehicle = row[3].GetString();

                    replayInfo.AddVehicle(vehicleId, new Vehicle(vehicleId, name, classVehicle));
                    break;
                }

                // Добавление игроков из реплея
                case 3:
                {
                    // Поиск стороны игрока и идентификатора бота
                    var botNumber = row[1].GetInt32();
                    var side = replayInfo.Bots[botNumber];


                    var playerName = row[3].GetString();

                    // Поиск не пустого имени
                    if (string.IsNullOrEmpty(playerName))
                    {
                        continue;
                    }

                    // Поиск имени и клан тега
                    var (name, tag, cadetTag) = GetNameTag(playerName);

                    var steamId = ValidateSteamId(row[4].GetString());
                    if (steamId is null)
                    {
                        continue;
                    }

                    // Получаем SteamId игрока
                    _ = replayInfo.GetPlayer(steamId.GetValueOrDefault()) ??
                               replayInfo.CreatePlayer(steamId.GetValueOrDefault(), name, tag, botNumber, side);
                    
                    if (tag is not null && cadetTag is not null && !replayInfo.Squads.TryGetValue(tag, out _))
                    {
                        replayInfo.AddSquad(tag, cadetTag);
                    }

                    break;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Разделение имени на имя тег и кадетский тег
    /// </summary>
    /// <param name="playerName">Полное имя игрока</param>
    /// <returns>Имя, Тэг, Кадетский тэг</returns>
    private static (string name, string? tag, string? cadetTag) GetNameTag(string playerName)
    {
        string name;
        string? tag = null;
        string? cadetTag = null;

        // Если не находим совпадение возвращаем имя и пустой клантег (т.к. у игрока нет отряда),
        // иначе разделяем на имя и клантег
        if (MyRegex().Match(playerName) is not { Success: true } match)
        {
            name = playerName;
        }
        else
        {
            name = match.Groups[2].Value;

            // Разделяем найденный тег на курсантскую часть
            var tempTag = match.Groups[1].Value;
            if (CheckCadetTag().Match(tempTag) is { Success: true } tagMatch)
            {
                tag = tagMatch.Groups[1].Value;
            }
            else
            {
                tag = tempTag;
            }
            
            cadetTag = $"{tag}_c";
        }

        return (name, tag, cadetTag);
    }


    /// <summary>
    /// Валидация SteamId
    /// </summary>
    /// <param name="stringSteamId"></param>
    /// <returns></returns>
    private static long? ValidateSteamId(string? stringSteamId)
    {
        // Парсим SteamId
        if (!string.IsNullOrEmpty(stringSteamId) && long.TryParse(stringSteamId, out var steamId) && steamId != 0)
        {
            return steamId;
        }

        return null;
    }



    /// <summary>
    /// Получение информации по килам с игры в 1 тайминг
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="replayEvent">События реплее</param>
    /// <param name="oneTime">Один тайминг</param>
    /// <param name="replayInfo">Объект текущего реплея</param>
    private static void GetKillsInfo(this ReplayInfo replayInfo, JsonElement replayEvent, Game game, IReadOnlyList<object> oneTime)
    {
        Vehicle? vehicle = null;
         if (!replayInfo.Players.TryGetValue(replayEvent[3].GetInt32(), out var killed) && 
             !replayInfo.Vehicles.TryGetValue(replayEvent[3].GetInt32(), out vehicle))
         {
             return;
         }
         if (!replayInfo.Players.TryGetValue(replayEvent[2].GetInt32(), out var murder))
         {
             return;
         }
        
        // Находим сторону игрока
        var side = killed?.Side ?? 0;

        // Находим противоположную сторону
        var oppositeSide = murder?.Side ?? 0;
        if (murder is null)
        {
            return;
        }

        // Идентификатор убийцы
        var steamIdMurder = murder.SteamId;

        // Идентификатор убитого
        var steamIdKilled = killed?.SteamId;

        // Если идентификатор убийцы не пустой
        if (steamIdMurder is 0)
        {
            return;
        }

        var isTeamKill = side == oppositeSide;
        var weapon = replayEvent[4].GetString();
        var timeMoment = oneTime[0] is JsonElement time ? time.GetInt32() : 0;
        var distance = replayEvent[6].GetDouble();
        var vehicleName = vehicle?.Name;
        var isVehicleKilled = vehicle is not null;

        replayInfo.AddKill(new Kill(isTeamKill, weapon, game, timeMoment, distance, vehicleName, isVehicleKilled,
            steamIdMurder, steamIdKilled, murder.Tag));
    }

    /// <summary>
    /// Получение информации по урону с игры в 1 тайминг
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="replayEvent">События реплее</param>
    /// <param name="oneTime">Один тайминг</param>
    /// <param name="replayInfo">Объект текущего реплея</param>
    private static void GetDamageInfo(this ReplayInfo replayInfo, JsonElement replayEvent, Game game,  IReadOnlyList<object> oneTime)
    {
        if (!replayInfo.Players.TryGetValue(replayEvent[2].GetInt32(), out var murder))
        {
            return;
        }
        
        if (!replayInfo.Players.TryGetValue(replayEvent[3].GetInt32(), out var killed))
        {
            return;
        }
        
        // Находим сторону игрока
        var side =  killed?.Side ?? 0;

        // Находим противоположную сторону
        var oppositeSide =  murder.Side;

        // Идентификатор убийцы
        var steamIdMurder = murder.SteamId;

        // Идентификатор убитого
        var steamIdKilled = killed?.SteamId;

        // Техника если есть
        Vehicle? v = null;
        if (steamIdKilled == null && replayInfo.Vehicles.TryGetValue(replayEvent[3].GetInt32(), out var vehicle))
        {
            v = vehicle;
        }

        // Урон
        var damage = replayEvent[7].GetDouble();
        
        // Пули
        var bullets = replayEvent[9].GetString(); 

        // Является ли этот кил тимкилом
        var isTeamKill = side == oppositeSide;
        
        // Оружие
        var weapon = replayEvent[4].GetString();
        
        // Время убийства
        var timeMoment = oneTime[0] is JsonElement time ? time.GetInt32() : 0;
        
        // Дистанция
        var distance = replayEvent[6].GetDouble();
        
        // Имя выбитой техники
        var vehicleName = v?.Name;
        
        // Уничтожение техники
        var isVehicleKilled = v is not null;

        // Если идентификатор убийцы не пустой
        if (steamIdMurder != 0)
        {
            replayInfo.AddDamage(new Damage(isTeamKill, weapon, game, steamIdMurder, steamIdKilled.GetValueOrDefault(),
                timeMoment, distance, vehicleName, isVehicleKilled, bullets, damage));
        }
    }

    /// <summary>
    /// Получение информации по лечению с игры в 1 тайминг
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="replayEvent">События реплее</param>
    /// <param name="oneTime">Один тайминг</param>
    /// <param name="replayInfo">Объект текущего реплея</param>
    private static void GetMedicalInfo(this ReplayInfo replayInfo, JsonElement replayEvent, Game game, IReadOnlyList<object> oneTime)
    {
            
        // Находим лечащего игрока
        if (!replayInfo.Players.TryGetValue(replayEvent[2].GetInt32(), out var healer))
        {
            return;
        }
        
        // Находим раненого игрока
        if (!replayInfo.Players.TryGetValue(replayEvent[3].GetInt32(), out var wounded))
        {
            return;
        }
        
        
        // Идентификатор лечащего
        var healerSteamId = healer?.SteamId;

        // Идентификатор раненого
        var woundedSteamId = wounded?.SteamId;

        // Мед средства используемые для лечения
        var medicalAffiliation = replayEvent[4].GetString();
        
        // Количество вылеченных поинтов
        var healthPointsHealed = replayEvent[5].GetDouble();

        // Если идентификатор убийцы не пустой
        if (healerSteamId != null && healerSteamId != 0)
        {
            replayInfo.AddMedicalInfo(
                new MedicalInfo(
                    game,
                    healerSteamId.GetValueOrDefault(),
                    woundedSteamId,
                    oneTime[0] is JsonElement time ? time.GetInt32() : 0,
                    healthPointsHealed,
                    medicalAffiliation.ToMedicalEnum()
                )
            );
        }
    }

    private  static MedicalAffiliation ToMedicalEnum(this string? medicalAffiliation)
    {
        return medicalAffiliation switch
        {
            "bandege" => MedicalAffiliation.Bandage,
            "medkit" => MedicalAffiliation.MedKit,
            "cpr" => MedicalAffiliation.Cpr,
            _ => MedicalAffiliation.None
        };
    }

    /// <summary>
    /// Поиск тега в никнейме
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"\[(.+)\](.+)")]
    private static partial Regex MyRegex();


    /// <summary>
    /// Поиск в строке кадетский тег _с
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("(.+)(_.*)")]
    private static partial Regex CheckCadetTag();
}