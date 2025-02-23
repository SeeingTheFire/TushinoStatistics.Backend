using Domain.Statistics.Entities;

namespace DataBase.Statistics.Models.DataTransferObjects;

/// <summary>
/// Модель представления игрока
/// </summary>
public class PlayerDto
{
    public PlayerDto(string name, string? tag, int kills, int deaths, int attendances, int teamKills, int survive,
        double medicalEfficiency, int deathsByTeamKill, DateTime lastDateUpdate)
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
        LastDateUpdate = lastDateUpdate;
        Efficiency = Deaths == 0 ? 0.0 : (double)Kills / Deaths;
    }

    public PlayerDto(Player player)
    {
        Kills = player.Kills.Count;
        TeamKillsDeath = player.Deaths.Count(kill => kill.TeamKill);
        Name = player.Name;
        Deaths = player.Deaths.Count;
        Attendances = player.Attendances.Count;
        Tag = player.Tag;
        Survive = player.Attendances.Count(x => !x.IsDead);
        MedicalEfficiency = player.TreatmentFromPlayer.Count(x => x.MedicalAffiliation == MedicalAffiliation.MedKit) * 0.17 +
                            player.TreatmentFromPlayer.Count(x => x.MedicalAffiliation == MedicalAffiliation.Bandage) * 0.006 +
                            player.TreatmentFromPlayer.Count(x => x.MedicalAffiliation == MedicalAffiliation.Cpr) * 0.09;
        TeamKills = player.Kills.Count(kill => kill.TeamKill);
        LastDateUpdate = DateTime.Now;
        Csv = Kills / (double)Attendances;
        Efficiency = Csv + MedicalEfficiency + CoefficientEfficiently * Survive - TeamKills / (double)Kills -
                     TeamKills / (double)Attendances;
    }

    /// <summary>
    /// Коэффициент эффективности
    /// </summary>
    private const double CoefficientEfficiently = 0.007;

    /// <summary>
    /// ЧСВ игрока
    /// </summary>
    public double Csv { get; set; }

    /// <summary>
    /// Очки за мед. действия
    /// </summary>
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