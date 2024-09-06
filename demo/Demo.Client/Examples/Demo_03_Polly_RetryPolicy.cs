using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Telemetry;

namespace Demo.Client.Examples;

public static class Demo03DelegatingHandler
{
    public static void Demo03_Retry(this IServiceCollection services)
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
                    MaxRetryAttempts = 3,
                    Delay = TimeSpan.FromSeconds(1),
                    BackoffType = DelayBackoffType.Constant,
                    UseJitter = false,
                    // DelayGenerator = static args =>
                    // {
                    //     var delay = args.AttemptNumber switch
                    //     {
                    //         0 => TimeSpan.Zero,
                    //         1 => TimeSpan.FromSeconds(1),
                    //         2 => TimeSpan.FromSeconds(5),
                    //         _ => TimeSpan.FromSeconds(10),
                    //     };
                    //
                    //     return new ValueTask<TimeSpan?>(delay);
                    // },
                    MaxDelay = TimeSpan.FromSeconds(30),
                });

                x.AddRetry(new HttpRetryStrategyOptions()
                {
                    Name = "Inner constant",
                    MaxRetryAttempts = 3,
                    Delay = TimeSpan.FromMilliseconds(500),
                    BackoffType = DelayBackoffType.Constant
                });
            });
    }
}