namespace DataBase.Statistics.Models;

/// <summary>
/// Десерализованный реплей
/// </summary>
public class ReplayInformation
{
    public List<List<object>> repl { get; init; } = null!;
}