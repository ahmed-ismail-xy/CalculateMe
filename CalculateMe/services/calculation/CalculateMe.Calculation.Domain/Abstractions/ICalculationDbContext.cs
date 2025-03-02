using CalculateMe.Calculation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculateMe.Calculation.Domain.Abstractions;

public interface ICalculationDbContext
{
    public DbSet<SummationTask> SummationTasks { get; set; }
}
