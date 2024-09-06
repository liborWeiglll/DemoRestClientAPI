using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace Demo.Client.Examples;

public static class Demo07Hedging
{
    public static void Demo07_Hedging(this IServiceCollection services)
    {
        services.AddHttpClient<ApiClient>(hc =>
        {
            hc.BaseAddress =
                new Uri("https://localhost:7050/api/demo/slow");
        })
        .AddResilienceHandler("Demo", x =>
        {
            x.AddHedging(new HttpHedgingStrategyOptions()
            {
                MaxHedgedAttempts = 4,
                Delay = TimeSpan.FromMilliseconds(200)
            });
        });
    }
}