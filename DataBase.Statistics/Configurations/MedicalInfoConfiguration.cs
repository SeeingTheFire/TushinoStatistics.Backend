using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Statistics.Configurations;

public class MedicalInfoConfiguration : IEntityTypeConfiguration<MedicalInfo> 
{
    public void Configure(EntityTypeBuilder<MedicalInfo> builder)
    {
        builder.ToTable("medical_info");
        builder.HasKey(x => x.UniqueIdentifier);
        
        builder.HasOne(x => x.Game)
            .WithMany(x=> x.MedicalInfos);

        builder.Property(e => e.MedicalAffiliation).IsRequired();
        builder.HasIndex(x => x.HealerSteamId);
        builder.HasIndex(x => x.WoundedSteamId);
    }
}