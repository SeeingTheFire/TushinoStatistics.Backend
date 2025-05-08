using Components.Statistics.Services.DomainServices;
using Components.Statistics.Services.Interfaces;
using DataBase.Statistics.Repositories;
using DataBase.Statistics.Repositories.Interfaces;
using Parser.Statistics.Services;
using Parser.Statistics.Services.Interfaces;
using Parser.Statistics.Services.Parser;

namespace Backend.Statistics;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IJsonLoaderService, JsonLoaderServiceFromHtml>();
        services.AddTransient<IJsonReplayParser, JsonReplayParser>();
        services.AddTransient<IReplayProcessingService, ReplayProcessingService>();
        
        services.AddTransient<IReplayService, ReplayService>();
        services.AddTransient<IPlayerService, PlayerService>();
        services.AddTransient<ISquadService, SquadService>();
        services.AddTransient<IKillsService, KillsService>();
        services.AddTransient<IAttendancesService, AttendancesService>();
        services.AddTransient<IMedicalService, MedicalService>();
        services.AddTransient<IVehicleService, VehicleService>();

        return services;

    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ISquadRepository, SquadRepository>();
        services.AddTransient<IPlayerRepository, PlayerRepository>();
        services.AddTransient<IReplayRepository, ReplayRepository>();
        services.AddTransient<IKillsRepository, KillsRepository>();
        services.AddTransient<IAttendancesRepository, AttendancesRepository>();
        services.AddTransient<IMedicalRepository, MedicalRepository>();
        services.AddTransient<IVehicleRepository, VehicleRepository>();

        return services;
    }
    
}