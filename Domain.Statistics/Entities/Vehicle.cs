namespace Domain.Statistics.Entities;

/// <summary>
/// Техника
/// </summary>
public class Vehicle(long id, string? name, string? classVehicle)
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; } = id;

    /// <summary>
    /// Никнейм
    /// </summary>
    public string? Name { get; } = name;

    /// <summary>
    /// Тип техники
    /// </summary>
    public string? ClassVehicle { get; set; } = classVehicle;
}