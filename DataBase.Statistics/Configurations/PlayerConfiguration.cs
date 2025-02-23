using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Statistics.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player> 
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");
        builder.HasKey(x => x.SteamId);
        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Ignore(x => x.Side);

        builder.HasMany(x => x.Kills)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Attendances)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.TreatmentToPlayer)
            .WithOne(x => x.Healer)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.TreatmentFromPlayer)
            .WithOne(x => x.Wounded)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Deaths)
            .WithOne(x => x.KilledUser);
        
        builder.Property(x => x.Tag)
            .HasMaxLength(15);

        builder.HasIndex(x=>x.Name);
        
        builder.Property(p => p.RowVersion)
            .HasColumnType("xid")
            .HasColumnName("xmin")
            .IsRowVersion();
    }
}