using Domain.Statistics.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Statistics.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance> 
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable("attendance");
        builder.HasKey(x => x.UniqueIdentifier);
        
        builder.HasOne(x => x.Game)
            .WithMany(x=> x.Attendances);

        builder.HasMany(x => x.Damages)
            .WithOne(x => x.Attendance);

        builder.HasIndex(x => x.UserSteamId);
    }
}