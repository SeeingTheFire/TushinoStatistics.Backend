namespace Domain.Statistics.Entities;

public class Attendance
{
    private Attendance() { }

    /// <summary>
    /// Конструктор посещаемости игрока
    /// </summary>
    /// <param name="gameId">Игра</param>
    /// <param name="userSteamId"></param>
    /// <param name="date"></param>
    /// <param name="isDead"></param>
    /// <param name="damages"></param>
    public Attendance(Game game, long userSteamId, bool isDead, List<Damage> damages)
    {
        UniqueIdentifier = Guid.NewGuid();
        Game = game;
        GameDate = game.Date;
        UserSteamId = userSteamId;
        IsDead = isDead;
        Damages = damages;
        Date = game.Date;
    }

    public DateTime Date { get; set; }

    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid UniqueIdentifier { get; init; }

    /// <summary>
    /// Идентификатор игры
    /// </summary>
    public Game Game { get; init; }
    
    /// <summary>
    /// Идентификатор игры
    /// </summary>
    public DateTime GameDate { get; init; }

    /// <summary>
    /// Стим Id игрока
    /// </summary>
    public long UserSteamId { get; init; }
    
    /// <summary>
    /// Был ли убит на этой игре
    /// </summary>
    public bool IsDead { get; init; }

    /// <summary>
    /// Игрок
    /// </summary>
    public Player? User { get; init; }

    /// <summary>
    /// Урон
    /// </summary>
    public List<Damage> Damages { get; init; }
}