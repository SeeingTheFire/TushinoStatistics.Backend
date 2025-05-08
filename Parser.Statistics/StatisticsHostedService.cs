using Parser.Statistics.Services.Interfaces;

namespace Parser.Statistics;

public class StatisticsHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<StatisticsHostedService> logger) : BackgroundService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Начало расчета статистики");
            using var serviceScope = serviceScopeFactory.CreateScope();
            var replayProcessingService = serviceScope.ServiceProvider.GetService<IReplayProcessingService>();
            
            // Обновляем информацию о реплеях
            await replayProcessingService.UpdateReplaysCollection(stoppingToken);

            logger.LogInformation("Окончание расчета статистики");

            //await Task.Delay(10000, stoppingToken);
            logger.LogInformation("Конец ожидания");
        }
    }
}