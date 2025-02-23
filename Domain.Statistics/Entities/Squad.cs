namespace Domain.Statistics.Entities;

/// <summary>
/// Отряд
/// </summary>
public class Squad
{
    public Squad(string tag, string cadetTag)
    {
        Tag = tag;
        CadetTag = cadetTag;
    }

    /// <summary>
    /// Конструктор для EFCore
    /// </summary>
    private Squad() { }
    
    /// <summary>
    /// Тег отряда
    /// </summary>
    public string Tag { get; private set; } 

    /// <summary>
    /// Курсантский тег
    /// </summary>
    public string CadetTag { get; private set; }

    /// <summary>
    /// Игроки Отряда
    /// </summary>
    public List<Player> Players { get; init; } = [];

    /// <summary>
    /// Версия для конкурентности
    /// </summary>
    public uint RowVersion { get; set; }

    public void Update(Squad replaySquad)
    {
        Tag = replaySquad.Tag;
        CadetTag = replaySquad.CadetTag;
    }

    public static bool operator ==(Squad left, Squad right)
    {
        return left.Tag == right.Tag && left.CadetTag == right.CadetTag;
    }

    public static bool operator !=(Squad left, Squad right)
    {
        return !(left == right);
    }
    
    protected bool Equals(Squad other)
    {
        return Tag == other.Tag && CadetTag == other.CadetTag && Players.Equals(other.Players) && RowVersion == other.RowVersion;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Squad)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Tag, CadetTag, Players, RowVersion);
    }
}