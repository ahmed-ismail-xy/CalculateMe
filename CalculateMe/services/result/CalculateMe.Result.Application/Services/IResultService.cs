using CalculateMe.Result.Domain.Entities;

namespace CalculateMe.Result.Application.Services;

public interface IResultService
{
    Task<TotalResult> GetResultAsync(CancellationToken cancellationToken = default);

    Task CalculateAsync(decimal number, CancellationToken cancellationToken = default);
}
