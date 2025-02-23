using Domain.Statistics.Entities;

namespace DataBase.Statistics.Models;

/// <summary>
/// Информация из 1 реплея
/// </summary>
public class ReplayInfo(Game game)
{
    /// <summary>
    /// Номер бота -> (игрок)
    /// </summary>
    public readonly Dictionary<int, Player> Players = new();
    
    /// <summary>
    /// SteamId -> Игрок
    /// </summary>
    public readonly Dictionary<long, Player> PlayersDictionary = new();

    /// <summary>
    /// Номер бота -> (техника)
    /// </summary>
    public readonly Dictionary<int, Vehicle> Vehicles = new();

    /// <summary>
    /// Номер бота / сторона бота
    /// </summary>
    public readonly Dictionary<int, int> Bots = new();

    /// <summary>
    /// Отряды
    /// </summary>
    public readonly Dictionary<string, Squad> Squads = [];

    // Килы, посещаемость, урон, мединцина
    private readonly HashSet<Kill> _kills = [];
    private readonly HashSet<Damage> _damageInfos = [];
    private readonly HashSet<MedicalInfo> _medicalsInfo = [];

    /// <summary>
    /// 
    /// </summary>
    public IReadOnlySet<Kill> Kills => _kills;

    /// <summary>
    /// 
    /// </summary>
    public IReadOnlySet<MedicalInfo> MedicalsInfo => _medicalsInfo;

    /// <summary>
    /// 
    /// </summary>
    public IReadOnlySet<Damage> DamageInfos => _damageInfos;

    // Идентификатор игры
    public string GameId { get; } = game.GameId;

    public Game Game { get; set; } = game;

    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="botNumber">Номер бота</param>
    /// <param name="player">Информация об игроке Id бота и Id стороны</param>
    /// <param name="steamId"></param>
    public void AddPlayer(int botNumber, Player player, long steamId)
    {
        Players[botNumber] = player;
        PlayersDictionary[steamId] = player;
    }

    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="idBot"></param>
    /// <param name="side"></param>
    public void AddBot(int idBot, int side)
    {
        Bots[idBot] = side;
    }

    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="idVehicle"></param>
    /// <param name="vehicle"></param>
    public void AddVehicle(int idVehicle, Vehicle vehicle)
    {
        Vehicles[idVehicle] = vehicle;
    }

    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="kill"></param>
    public void AddKill(Kill kill)
    {
        _kills.Add(kill);
    }

    /// <summary>
    /// Добавление Дамага
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(Damage damage)
    {
        _damageInfos.Add(damage);
    }

    /// <summary>
    /// Инфы о лечении
    /// </summary>
    /// <param name="medicalInfo"></param>
    public void AddMedicalInfo(MedicalInfo medicalInfo)
    {
        _medicalsInfo.Add(medicalInfo);
    }

    public void AddSquad(string tag, string cadetTag)
    {
        if (!Squads.TryGetValue(tag, out _))
        {
            Squads[tag] = new Squad(tag, cadetTag);
        }
    }

    public Player? GetPlayer(long steamId)
    {
        PlayersDictionary.TryGetValue(steamId, out var player);
        return player;
    }

    public Player CreatePlayer(long steamId, string name, string? tag, int botNumber, int side)
    {
        var player = new Player(steamId, name, tag) {Side = side};
        AddPlayer(botNumber, player, steamId);
        return player;
    }
}