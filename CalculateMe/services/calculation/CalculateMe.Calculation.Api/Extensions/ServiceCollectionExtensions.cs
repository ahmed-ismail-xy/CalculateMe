using CalculateMe.Calculation.Application;
using CalculateMe.Calculation.Infrastructure;
using System.Text.Json.Serialization;

namespace CalculateMe.Calculation.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddApplication();

        services.AddControllersWithJsonOptions();

        services.AddProblemDetails();

        services.AddHealthChecks();

        services.AddHttpContextAccessor();

        return services;
    }

    private static IServiceCollection AddControllersWithJsonOptions(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        return services;
    }
}