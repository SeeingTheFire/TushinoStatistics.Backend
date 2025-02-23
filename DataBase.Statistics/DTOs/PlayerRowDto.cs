namespace DataBase.Statistics.DTOs;

public class PlayerRowDto
{
    public PlayerRowDto(string name, string? tag, int kills, int deaths, int attendances, int teamKills, int survive,
        double medicalEfficiency, int deathsByTeamKill, DateTime now)
    {
        Kills = kills;
        TeamKillsDeath = deathsByTeamKill;
        Name = name;
        Deaths = deaths;
        Attendances = attendances;
        Tag = tag;
        Survive = survive;
        MedicalEfficiency = medicalEfficiency;
        TeamKills = teamKills;
        LastDateUpdate = now;
        Efficiency = Deaths == 0 ? 0.0 : (double)Kills / Deaths;
    }

    public double MedicalEfficiency { get; set; }

    /// <summary>
    /// Имя Игрока
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Тег Отряда
    /// </summary>
    public string? Tag { get; set; }
    
    /// <summary>
    /// Количество убийств
    /// </summary>
    public int Kills { get; set; }
    
    /// <summary>
    /// Тимкиллы
    /// </summary>
    public int TeamKills { get; set; }
    
    /// <summary>
    /// Смерти от тимкилов
    /// </summary>
    public int TeamKillsDeath { get; set; }
    
    /// <summary>
    /// Выжил
    /// </summary>
    public int Survive { get; set; }
    
    /// <summary>
    /// Количество смертей
    /// </summary>
    public int Deaths { get; set; }
    
    /// <summary>
    /// Количество посещаемостей
    /// </summary>
    public int Attendances { get; set; }
    
    /// <summary>
    /// Эффективность игрока
    /// </summary>
    public double Efficiency { get; set; }
    
    /// <summary>
    /// Эффективность игрока
    /// </summary>
    public DateTime LastDateUpdate { get; set; }
}