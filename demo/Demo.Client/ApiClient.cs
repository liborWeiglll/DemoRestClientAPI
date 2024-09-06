using System.Net.Http.Json;
using System.Text.Json;
using Demo.Contracts;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using Polly.RateLimiting;
using Polly.Timeout;

namespace Demo.Client;

public class ApiClient(HttpClient client, ILogger<ApiClient> logger)
{
    public async Task<ApiResponse<List<ProductDto>>> GetProducts(CancellationToken ctn = default)
    {
        HttpResponseMessage response = null;

        try
        {
            response = await client.GetAsync("", ctn);
        }
        catch (BrokenCircuitException e)
        {
            return new(new List<ProductDto>());
        }
        catch (TaskCanceledException e)
        {
            return new($"{e.Message}");
        }
        catch (Exception e)
        {
            return new($"{e.Message}");
        }

        if (!response.IsSuccessStatusCode)
        {
            // todo: lze použít ProblemDetails
            // var error = await response.Content.ReadFromJsonAsync<ProblemDetails>(ctn);
            // return new(error);

            return new($"{response.StatusCode}");
        }

        if (response.Content.Headers.ContentType?.MediaType != "application/json")
        {
            return new("Invalid content type");
        }

        try
        {
            var data = await response.Content.ReadFromJsonAsync<List<ProductDto>>(
                new JsonSerializerOptions(JsonSerializerDefaults.Web), ctn);
            return new(data);
        }
        catch (Exception e)
        {
            return new(e.Message);
        }
    }
}