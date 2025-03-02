using Microsoft.EntityFrameworkCore;
using MassTransit;
using CalculateMe.Result.Domain.Abstractions;
using CalculateMe.Result.Domain.Entities;

namespace CalculateMe.Result.Infrastructure;

public sealed class ResultDbContext(DbContextOptions<ResultDbContext> options) 
    : DbContext(options), IResultDbContext, IUnitOfWork
{
    public DbSet<TotalResult> TotalResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ResultDbContext).Assembly);

        // Configure outbox tables
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}