namespace Domain.Statistics.Entities;

/// <summary>
/// Класс информации об 1 килле
/// </summary>
public class Kill
{
    /// <summary>
    /// Конструктор необходимый для EF Core
    /// </summary>
    private Kill()
    {
    }
    
    public Kill(bool teamKill, string? weapons, Game game, int time, double distance, string? vehicleName,
        bool isVehicleKilled, long userSteamId, long? killedSteamId, string? userTag)
    {
        UniqueIdentifier = Guid.NewGuid();
        Game = game;
        UserSteamId = userSteamId;
        KilledSteamId = killedSteamId;
        UserTag = userTag;
        Weapons = weapons;
        Time = time;
        Distance = distance;
        TeamKill = teamKill;
        VehicleName = vehicleName;
        IsVehicleKilled = isVehicleKilled;
        Date = game.Date;
    }

    public DateTime Date { get; set; }

    /// <summary>
    /// Тимкилл
    /// </summary>
    public bool TeamKill { get; init; }

    /// <summary>
    /// Оружие
    /// </summary>
    public string? Weapons { get; init; }

    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid UniqueIdentifier { get; init; }

    /// <summary>
    /// Идентификатор игры
    /// </summary>
    public Game Game { get; init; }

    /// <summary>
    /// Стим Id убийцы
    /// </summary>
    public long UserSteamId { get; init; }

    /// <summary>
    /// Тэг отряда
    /// </summary>
    public string? UserTag { get; init; }
    
    /// <summary>
    /// Игрок убийца
    /// </summary>
    public Player? User { get; init; }

    /// <summary>
    /// Стим Id убитого
    /// </summary>
    public long? KilledSteamId { get; init; }

    /// <summary>
    /// Убитый игрок
    /// </summary>
    public Player? KilledUser { get; init; }

    /// <summary>
    /// Время
    /// </summary>
    public int Time { get; init; }

    /// <summary>
    /// Дистанция
    /// </summary>
    public double Distance { get; init; }

    /// <summary>
    /// Имя выбитой техники
    /// </summary>
    public string? VehicleName { get; init; }

    /// <summary>
    /// Если это килл техники
    /// </summary>
    public bool IsVehicleKilled { get; init; }

    /// <summary>
    /// Урон
    /// </summary>
    public List<Damage> Damages { get; set; }
}