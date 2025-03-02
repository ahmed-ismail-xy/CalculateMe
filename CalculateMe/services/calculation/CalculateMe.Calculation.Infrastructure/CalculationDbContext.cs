using CalculateMe.Calculation.Domain.Abstractions;
using CalculateMe.Calculation.Domain.Entities;
using CalculateMe.Calculation.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using MassTransit;

namespace CalculateMe.Calculation.Infrastructure;

public sealed class CalculationDbContext(DbContextOptions<CalculationDbContext> options) 
    : DbContext(options), ICalculationDbContext, IUnitOfWork
{
    public DbSet<SummationTask> SummationTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new SummationTaskConfiguration());

        // Configure outbox tables
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}