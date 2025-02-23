namespace DataBase.Statistics.Models;

public class JsonReplayRowDto
{
    // Строки реплея каждые 5 секунд
    public List<Row> Rows { get; init; } = null!;
    
    // Всего строк
    public int Total { get; init; }
    
    // Источник
    public string Source { get; init; } = null!;
    
    // Был ли записан с ошибкой
    public string Error { get; init; } = null!;
}