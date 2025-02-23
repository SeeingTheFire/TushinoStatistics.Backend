namespace Domain.Statistics.Entities;

public class MedicalInfo
{
    private MedicalInfo()
    {
        
    }
    
    public MedicalInfo(Game game,
        long healerSteamId,
        long? woundedSteamId,
        int timeSecond,
        double healthPointsHealed,
        MedicalAffiliation medicalAffiliation)
    {
        MedicalAffiliation = medicalAffiliation;
        Game = game;
        Date = game.Date;
        HealerSteamId = healerSteamId;
        WoundedSteamId = woundedSteamId;
        TimeSecond = timeSecond;
        HealthPointsHealed = healthPointsHealed;
    }

    /// <summary>
    /// Медицинская принадлежность
    /// </summary>
    public MedicalAffiliation MedicalAffiliation { get; init; }

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
    public DateTime Date { get; init; }

    /// <summary>
    /// Стим Id лечащего
    /// </summary>
    public long HealerSteamId { get; init; }

    /// <summary>
    /// Лечащий игрок
    /// </summary>
    public Player Healer { get; init; }
    
    /// <summary>
    /// Стим Id раненого
    /// </summary>
    public long? WoundedSteamId { get; init; }

    /// <summary>
    /// Раненый игрок
    /// </summary>
    public Player? Wounded { get; init; }
    
    /// <summary>
    /// Время
    /// </summary>
    public int TimeSecond { get; init; }

    /// <summary>
    /// Очки здоровья вылеченные
    /// </summary>
    public double HealthPointsHealed { get; init; }
}
