using DataBase.Statistics.Configurations;
using DataBase.Statistics.Models;
using Domain.Statistics.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Version = Domain.Statistics.Entities.Version;

namespace DataBase.Statistics;

public sealed class ApplicationContext : IdentityDbContext<ApplicationUser>
{

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
    
    /// <summary>
    /// Версии бд
    /// </summary>
    public DbSet<Version> Versions { get; set; } = null!;
    
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<Player> Players { get; set; } = null!;

    /// <summary>
    /// Реплеи
    /// </summary>
    public DbSet<Game> Replays { get; set; } = null!;

    /// <summary>
    /// Информация об убийствах
    /// </summary>
    public DbSet<Kill> Kills { get; set; } = null!;

    /// <summary>
    /// Информация о посещаемости
    /// </summary>
    public DbSet<Attendance> Attendance { get; set; } = null!;

    /// <summary>
    /// Информация о посещаемости
    /// </summary>
    public DbSet<MedicalInfo> MedicineInfo { get; set; } = null!;
    
    /// <summary>
    /// Отряды
    /// </summary>
    public DbSet<Squad> Squads { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfigurationsAnchor).Assembly);
        
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v, v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
            foreach (var property in entityType.GetProperties()) {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    property.SetValueConverter(dateTimeConverter);
            }
        }
        
        base.OnModelCreating(modelBuilder);
    }
}