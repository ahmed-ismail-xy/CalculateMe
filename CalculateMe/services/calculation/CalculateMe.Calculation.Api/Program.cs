using CalculateMe.Calculation.Api.Extensions;

namespace CalculateMe.Calculation.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.ConfigureSerilog();

        builder.Services.AddServices(builder.Configuration);

        var app = builder.Build();

        app.UseAppDefaults();

        app.Run();
    }
}
