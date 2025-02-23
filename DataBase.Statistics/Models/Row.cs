namespace DataBase.Statistics.Models;

public class Row
{
    // Имя реплея
    public string Name { get; set; } = null!;
    
    // Размер архива
    public int Archive { get; set; }
    
    // Размер файла
    public int FileSize { get; set; }
    
    // Массив значений одной строки
    public List<string> Array { get; set; } = null!;
}