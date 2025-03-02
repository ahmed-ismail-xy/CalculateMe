using CalculateMe.Result.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculateMe.Result.Domain.Abstractions;

public interface IResultDbContext
{
    public DbSet<TotalResult> TotalResults { get; set; }
}
