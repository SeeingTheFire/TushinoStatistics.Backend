namespace Domain.Statistics.Entities;

public class Game
{
    /// <summary>
    /// Имя игры
    /// </summary>
    public string Name { get; init; } = null!;
        
    /// <summary>
    /// Дата игры
    /// </summary>
    public DateTime Date { get; init; }
        
    /// <summary>
    /// Карта
    /// </summary>
    public string Map { get; init; } = null!;
        
    /// <summary>
    /// Идентификатор игры
    /// </summary>
    public string GameId { get; init; } = null!;
        
    /// <summary>
    /// Идентификатор сервера
    /// </summary>
    public string Server { get; init; } = null!;
    
    /// <summary>
    /// Игроки
    /// </summary>
    public List<Player> Players { get; init; } = [];

    public IEnumerable<MedicalInfo>? MedicalInfos { get; set; }
    public IEnumerable<Kill>? Kills { get; set; }
    public IEnumerable<Attendance>? Attendances { get; set; }
    public IEnumerable<Damage>? Damages { get; set; }
}