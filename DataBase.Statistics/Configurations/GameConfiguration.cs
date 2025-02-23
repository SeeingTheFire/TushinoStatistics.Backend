using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Statistics.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game> 
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("games");
        builder.HasKey(x => x.GameId);
        builder.Property(x => x.Name)
            .HasMaxLength(300);
        
        builder.Property(x => x.Map)
            .HasMaxLength(300);
        
        builder.Property(x => x.GameId)
            .HasMaxLength(300);
        
        builder.Property(x => x.Server)
            .HasMaxLength(20);

        builder.HasIndex(x => x.Date);

        builder.HasMany(x => x.Players)
            .WithMany(x => x.Games);
    }
}