using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CalculateMe.Result.Domain.Entities;

namespace CalculateMe.Result.Infrastructure.Configurations;

public class TotalResultConfiguration : IEntityTypeConfiguration<TotalResult>
{
    public void Configure(EntityTypeBuilder<TotalResult> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Result).IsRequired();
        
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.UpdatedAt).IsRequired(false);
    }
}
