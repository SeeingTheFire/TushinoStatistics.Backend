using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Version = Domain.Statistics.Entities.Version;

namespace DataBase.Statistics.Configurations;

public class VersionConfiguration : IEntityTypeConfiguration<Version> 
{
    public void Configure(EntityTypeBuilder<Version> builder)
    {
        builder.HasKey(x => x.VersionNumber);
        builder.Property(x => x.VersionNumber).ValueGeneratedOnAdd();
    }
}