using CalculateMe.Result.Application.Services;
using Grpc.Core;

namespace CalculateMe.Result.Api.Services;

public class ResultGrpcService : ResultService.ResultServiceBase
{
    private readonly IResultService _resultService;

    public ResultGrpcService(IResultService resultService)
    {
        _resultService = resultService;
    }

    public override async Task<ResultResponse> GetResult(ResultRequest request, ServerCallContext context)
    {
        var result = await _resultService.GetResultAsync(context.CancellationToken);

        return new ResultResponse
        {
            Total = result.Result // Ensure property name matches the proto definition
        };
    }
}
