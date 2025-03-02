using CalculateMe.Result.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CalculateMe.Result.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IResultService, ResultService>();

        return services;
    }
}
