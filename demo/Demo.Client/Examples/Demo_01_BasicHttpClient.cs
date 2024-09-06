using Microsoft.Extensions.DependencyInjection;

namespace Demo.Client.Examples;

public static class Demo01BasicHttpClient
{
    public static void Demo01_Basic(this IServiceCollection services)
    {
        services.AddHttpClient<ApiClient>(httpclient =>
        {
            httpclient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpclient.Timeout = TimeSpan.FromSeconds(5);
            httpclient.BaseAddress = new Uri("https://localhost:7050/api/products");
        }).SetHandlerLifetime(TimeSpan.FromMinutes(30));
    }
}