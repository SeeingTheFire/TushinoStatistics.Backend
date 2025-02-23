using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Statistics.Configurations;

public class KillConfiguration : IEntityTypeConfiguration<Kill> 
{
    public void Configure(EntityTypeBuilder<Kill> builder)
    {
        builder.ToTable("kills");
        builder.HasKey(x => x.UniqueIdentifier);
        builder.Property(x => x.Weapons)
            .HasMaxLength(300);
        
        builder.HasOne(x => x.Game)
            .WithMany(x=> x.Kills);
        
        builder.Property(x => x.UserTag)
            .HasMaxLength(15);
        
        builder.Property(x => x.VehicleName)
            .HasMaxLength(300);
        
        builder.HasMany(x => x.Damages)
            .WithOne(x => x.Kill);

        builder.HasIndex(x => x.KilledSteamId);
        builder.HasIndex(x => x.UserSteamId);
    }
}