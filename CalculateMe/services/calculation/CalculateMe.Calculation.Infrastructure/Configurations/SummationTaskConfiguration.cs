using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CalculateMe.Calculation.Domain.Entities;

namespace CalculateMe.Calculation.Infrastructure.Configurations;

public class SummationTaskConfiguration : IEntityTypeConfiguration<SummationTask>
{
    public void Configure(EntityTypeBuilder<SummationTask> builder)
    {
        builder.ToTable("Calculations");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstNumber)
               .HasPrecision(18, 2);

        builder.Property(e => e.SecondNumber)
               .HasPrecision(18, 2);

        builder.Property(e => e.Result)
               .HasPrecision(18, 2);

        builder.Property(e => e.Status)
               .HasConversion<string>();
    }
}
