using System.Threading.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Demo.Client.Examples;

public static class Demo08RateLimiter
{
    public static void Demo08_RateLimiter(this IServiceCollection services)
    {
        services.AddHttpClient<ApiClient>(hc =>
        {
            hc.BaseAddress =
                new Uri("https://localhost:7050/api/demo/rate-limited");
        })
        .AddResilienceHandler("Demo", x =>
        {
            x.AddRateLimiter(new FixedWindowRateLimiter(
                new FixedWindowRateLimiterOptions()
                {
                    PermitLimit = 1,
                    Window = TimeSpan.FromSeconds(12),
                    QueueLimit = 1,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                }));
        });
    }
}