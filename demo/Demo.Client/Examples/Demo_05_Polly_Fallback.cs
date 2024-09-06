using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;

namespace Demo.Client.Examples;

public static class Demo05Fallback
{
    public static void Demo05_Fallback(this IServiceCollection services)
    {
        services.AddHttpClient<ApiClient>(hc =>
        {
            hc.BaseAddress =
                new Uri("https://localhost:7050/api/demo/time-failing");
        })
        .AddResilienceHandler("Demo", x =>
        {
            x.AddFallback(new FallbackStrategyOptions<HttpResponseMessage>()
            {
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .HandleResult(r => r.StatusCode == HttpStatusCode.InternalServerError),

                FallbackAction = static args =>
                {
                    var rm = args.Outcome.Result;
                    rm.StatusCode = HttpStatusCode.OK;
                    rm.Content = new StringContent("[]");
                    rm.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return ValueTask.FromResult(Outcome.FromResult(rm));
                }
            });

            // x.AddRetry(new HttpRetryStrategyOptions()
            // {
            //     Delay = TimeSpan.FromMilliseconds(500),
            //     BackoffType = DelayBackoffType.Linear,
            //     UseJitter = false,
            //     MaxRetryAttempts = 2
            // });

        });
    }
}