using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace Demo.Client.Examples;

public static class Demo06Timeout
{
    public static void Demo06_Timeout(this IServiceCollection services)
    {
        services.AddHttpClient<ApiClient>(hc =>
        {
            hc.BaseAddress =
                new Uri("https://localhost:7050/api/demo/timeout");
        })
        .AddResilienceHandler("Demo", x =>
        {
            // Fallback
            // Retry
            // CircuitBreaker

            x.AddTimeout(new HttpTimeoutStrategyOptions()
            {
                Timeout = TimeSpan.FromSeconds(3)
            });

        });
    }
}