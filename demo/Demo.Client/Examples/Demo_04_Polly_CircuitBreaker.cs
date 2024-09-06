using System.Net;
using System.Threading.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace Demo.Client.Examples;

public static class Demo04CircuitBreaker
{
    public static void Demo04_CircuitBreaker(this IServiceCollection services)
    {
        services.AddHttpClient<ApiClient>(hc =>
        {
            hc.BaseAddress =
                new Uri("https://localhost:7050/api/demo/time-failing");
        })
        .AddResilienceHandler("Demo", x =>
        {
            x.AddRetry(new HttpRetryStrategyOptions()
            {
                MaxRetryAttempts = 10,
                Delay = TimeSpan.FromMilliseconds(500),
                BackoffType = DelayBackoffType.Constant
            });

            x.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions()
            {
                // ShouldHandle = static args =>
                // {
                //     if (args.Outcome.Exception is OperationCanceledException)
                //         return ValueTask.FromResult(false);
                //
                //     if (args.Outcome.Exception != null)
                //         return ValueTask.FromResult(true);
                //
                //     if (!args.Outcome.Result?.IsSuccessStatusCode is true)
                //         return ValueTask.FromResult(true);
                //
                //     return ValueTask.FromResult(false);
                // },
                FailureRatio = 0.5,
                SamplingDuration = TimeSpan.FromSeconds(10),
                MinimumThroughput = 4, // 50+
                BreakDuration = TimeSpan.FromSeconds(30)
            });

        });
    }
}