using CalculateMe.Result.Domain.Abstractions;
using CalculateMe.Result.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalculateMe.Result.Application.Services;

public sealed class ResultService : IResultService
{
    private readonly IResultDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ResultService> _logger;

    public ResultService(
        IResultDbContext dbContext,
        IUnitOfWork unitOfWork,
        ILogger<ResultService> logger)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task CalculateAsync(decimal number, CancellationToken cancellationToken = default)
    {
        var totalResult = await GetResultAsync(cancellationToken);

        _logger.LogInformation("Calculating {Number} + {TotalResult}", number, totalResult.Result);

        totalResult.Calculate(number);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Total result {TotalResultId} saved", totalResult.Id);
    }

    public async Task<TotalResult> GetResultAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting total result");

        var totalResult = await _dbContext.TotalResults
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (totalResult == null)
        {
            _logger.LogInformation("Total result not found, creating new one");

            totalResult = TotalResult.Create(Guid.NewGuid(), 0);

            _dbContext.TotalResults.Add(totalResult);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Total result {TotalResultId} created", totalResult.Id);
        }

        return totalResult;
    }
}
