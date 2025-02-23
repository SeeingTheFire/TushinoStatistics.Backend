namespace Domain.Statistics.Entities;

/// <summary>
/// Игрок
/// </summary>
public class Player
{
    public Player(long steamId, string name, string? tag)
    {
        SteamId = steamId;
        Name = name;
        Tag = tag;
    }

    private Player() { }
    
    /// <summary>
    /// Стим Id
    /// </summary>
    public long SteamId { get; private set; } 
    
    /// <summary>
    /// Игры которые игрок сыграл
    /// </summary>
    public List<Game> Games { get; private set; } = [];

    /// <summary>
    /// Сторона
    /// </summary>
    public int Side { get; set; }
    
    /// <summary>
    /// Никнейм
    /// </summary>
    public string Name { get; set; } 
    /// <summary>
    /// Отряд
    /// </summary>
    public string? Tag { get; set; } 

    /// <summary>
    /// Отряд
    /// </summary>
    public Squad? Squad { get; init; }

    /// <summary>
    /// Убийства
    /// </summary>
    public List<Kill> Kills { get; init; } = [];
    
    /// <summary>
    /// Посещаемость
    /// </summary>
    public List<Attendance> Attendances { get; init; }= [];
    
    /// <summary>
    /// Лечения производимые игроком
    /// </summary>
    public List<MedicalInfo> TreatmentFromPlayer { get; init; }= [];
    
    /// <summary>
    /// Лечение игрока
    /// </summary>
    public List<MedicalInfo> TreatmentToPlayer { get; init; }= [];
    
    /// <summary>
    /// Летальные исходы
    /// </summary>
    public List<Kill> Deaths { get; init; }= [];

    public uint RowVersion { get; set; }

    public void Update(Player replayPlayer)
    {
        Name = replayPlayer.Name;
        Tag = replayPlayer.Tag;
    }
}