using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Statistics.Configurations;

public class SquadConfiguration : IEntityTypeConfiguration<Squad> 
{
    public void Configure(EntityTypeBuilder<Squad> builder)
    {
        builder.ToTable("squads");
        builder.HasKey(x => x.Tag);
        builder.Property(x => x.Tag)
            .HasMaxLength(10);
        
        builder.Property(x => x.CadetTag)
            .HasMaxLength(15);

        builder.HasMany(x => x.Players)
            .WithOne(x => x.Squad)
            .HasForeignKey(x=>x.Tag)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.RowVersion)
            .HasColumnType("xid")
            .HasColumnName("xmin")
            .IsRowVersion();
    }
}