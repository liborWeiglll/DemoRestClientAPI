using Microsoft.Extensions.DependencyInjection;

namespace Demo.Client.Examples;

public static class Demo02DelegatingHandler
{
    public static void Demo02_Handlers(this IServiceCollection services)
    {
        services.AddHttpClient<ApiClient>(hc =>
        {
            hc.BaseAddress =
                new Uri("https://localhost:7050/api/demo/failing");
        }).AddHttpMessageHandler<RetryHandler>();
    }
}