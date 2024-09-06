namespace Demo.Server.Chaos;

public class ChaosMiddleware(ChaosContext chaos) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        chaos.RequestCounter++;
        context.Response.Headers.Append("Request-Counter", chaos.RequestCounter.ToString());
        await next(context);
    }
}