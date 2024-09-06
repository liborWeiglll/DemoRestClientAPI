using System.Net;
using Microsoft.Extensions.Logging;

namespace Demo.Client;

public class RetryHandler(ILogger<RetryHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ctn)
    {
        int attempt = 1;
        int maxAttempts = 3;

        while(attempt <= maxAttempts)
        {
            try
            {
                var response = await base.SendAsync(request, ctn);
                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (Exception e)
            {
                logger.LogWarning("Request failed on attempt {attempt}/{max} with error {error}", attempt, maxAttempts, e.Message);
            }
            finally
            {
                attempt++;
            }
        }

        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
    }
}