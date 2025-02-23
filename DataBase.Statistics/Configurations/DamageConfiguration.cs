using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Statistics.Configurations;

public class DamageConfiguration: IEntityTypeConfiguration<Damage> 
{
    public void Configure(EntityTypeBuilder<Damage> b)
    {
        b.HasKey(x => x.UniqueIdentifier);
        b.Property(x => x.Weapons)
            .HasMaxLength(300);

        b.HasOne(x => x.Game)
            .WithMany(x=> x.Damages);

        b.Property(x => x.Weapons)
            .HasMaxLength(300);

        b.Property(x => x.VehicleName)
            .HasMaxLength(300);
        
        b.Property(x => x.BulletType)
            .HasMaxLength(300);
    }
}