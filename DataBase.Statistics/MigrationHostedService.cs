using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataBase.Statistics;

public class MigrationHostedService(IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var serviceScope = serviceScopeFactory.CreateScope();
        await using var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();
        context.Database.SetCommandTimeout(300);
        await context.Database.MigrateAsync(cancellationToken);
        context.Database.SetCommandTimeout(null);
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
}