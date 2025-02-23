namespace DataBase.Statistics.Models.DataTransferObjects;

/// <summary>
/// Модель представления отряда
/// </summary>
public class SquadDto
{
    public SquadDto(ICollection<PlayerDto> players, string tag)
    {
        Players = players;
        Tag = tag;
        Kills = players.Sum(x => x.Kills);
        Deaths = players.Sum(x => x.Deaths);
        Attendances = players.Sum(x => x.Attendances);
        Efficiency = Deaths == 0 ? 0.0 : (double)Kills / Deaths;
    }

    /// <summary>
    /// Игроки отряда
    /// </summary>
    public ICollection<PlayerDto> Players { get; set; }

    /// <summary>
    /// Тег Отряда
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    /// Количество убийств Отряда
    /// </summary>
    public int Kills { get; set; }

    /// <summary>
    /// Количество смертей Отряда
    /// </summary>
    public int Deaths { get; set; }

    /// <summary>
    /// Количество посещаемостей Отряда
    /// </summary>
    public int Attendances { get; set; }

    /// <summary>
    /// Эффективность Отряда
    /// </summary>
    public double Efficiency { get; set; }
}