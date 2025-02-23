namespace Parser.Statistics.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса по обработке реплеев
/// </summary>
public interface IReplayProcessingService
{
    /// <summary>
    /// Метод обновление информации о новых реплеях
    /// </summary>
    public Task UpdateReplaysCollection(CancellationToken cancellationToken);
}