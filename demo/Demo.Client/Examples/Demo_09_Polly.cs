using System.Net.Http.Json;
using Demo.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace Demo.Client.Examples;

public static class Demo09Polly
{
    public static async Task Demo09_PollyImpl(this IServiceProvider sp)
    {
        var pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions()
            {
                Delay = TimeSpan.FromSeconds(1),
                BackoffType = DelayBackoffType.Constant,
                MaxRetryAttempts = 3
            })
            .AddTimeout(TimeSpan.FromSeconds(1))
            .Build();

        using HttpClient client = sp.GetRequiredService<IHttpClientFactory>().CreateClient();
        var data = await pipeline.ExecuteAsync(async ctn =>
        {
            return await client.GetFromJsonAsync<List<ProductDto>>("https://localhost:7050/api/demo/failing", ctn);
        });

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Počet položek: {data.Count}");
    }
}