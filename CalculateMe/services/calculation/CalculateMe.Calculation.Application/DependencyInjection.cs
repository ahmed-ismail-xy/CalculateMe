using CalculateMe.Calculation.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CalculateMe.Calculation.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICalculationService, CalculationService>();

        return services;
    }
}
