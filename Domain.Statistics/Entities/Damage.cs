namespace Domain.Statistics.Entities;

public class Damage
{
    private Damage()
    {
        
    }
    public Damage(bool teamKill,
        string? weapons,
        Game game,
        long? murdererSteamId,
        long killedSteamId,
        int time,
        double distance,
        string? vehicleName,
        bool isVehicleKilled,
        string? bulletType,
        double damageValue)
    {
        DamageValue = damageValue;
        BulletType = bulletType;
        TeamKill = teamKill;
        Weapons = weapons;
        Game = game;
        Date = game.Date;
        MurdererSteamId = murdererSteamId;
        KilledSteamId = killedSteamId;
        Time = time;
        Distance = distance;
        VehicleName = vehicleName;
        IsVehicleKilled = isVehicleKilled;
    }

    /// <summary>
    /// Урон
    /// </summary>
    public double DamageValue { get; init; }

    /// <summary>
    /// Оружие
    /// </summary>
    public string? BulletType { get; init; }

    /// <summary>
    /// Тимкил
    /// </summary>
    public bool TeamKill { get; init; }

    /// <summary>
    /// Оружие
    /// </summary>
    public string? Weapons { get; init; }

    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid UniqueIdentifier { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор игры
    /// </summary>
    public Game Game { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Стим Id убийцы
    /// </summary>
    public long? MurdererSteamId { get; init; }

    /// <summary>
    /// Стим Id убитого
    /// </summary>
    public long KilledSteamId { get; init; }

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

    public Kill Kill { get; set; }
    public Attendance Attendance { get; set; }
}