using Demo.Contracts;
using Demo.Server.Chaos;
using Demo.Server.Products;

namespace Demo.Server.Endpoints;

public static class DemoApi
{
    public static RouteGroupBuilder MapDemoApi(this RouteGroupBuilder api)
    {
        api.MapGet("/products", async (ProductService service, CancellationToken ctn) =>
                Results.Ok((object) await service.GetProducts(ctn)))
            .Help("Standardní implementace");


        api.MapGet("/demo/slow", async (ProductService service, ChaosContext chaos) =>
            {
                chaos.SimulateSlowApi();

                return Results.Ok(await service.GetProducts());
            })
            .Help("Pomalé API, 2 ze 3 požadavků jsou pomalé");

        api.MapGet("/demo/timeout", async (ProductService service, ChaosContext chaos) =>
        {
            await Task.Delay(35_000);

            return Results.Ok(await service.GetProducts());
        }).Help("Odpověď trvá 35 sekund");


        api.MapGet("/demo/failing", async (ProductService service, ChaosContext chaos) =>
            {
                try
                {
                    chaos.SimulateFailingApi();
                }
                catch (Exception e)
                {
                    return Results.StatusCode(500);
                }

                return Results.Ok(await service.GetProducts());
            })
            .Help("Nespolehlivé API, 2 ze 3 požadavků selžou");


        api.MapGet("/demo/time-failing", async (ProductService service, ChaosContext chaos) =>
            {
                try
                {
                    chaos.SimulateTimeFailingApi();
                }
                catch (Exception e)
                {
                    return Results.StatusCode(500);
                }

                return Results.Ok(await service.GetProducts());
            })
            .Help("Požadavky selhávají když je aktuální sekunda > 10");

        api.MapGet("/demo/rate-limited", async (ProductService service, ChaosContext chaos) =>
            {
                // simulace pomalejšího API
                await Task.Delay(300);

                if (chaos.IsRateLimitExceeded(TimeSpan.FromSeconds(10)))
                {
                    return Results.StatusCode(429);
                }

                return Results.Ok(await service.GetProducts());
            })
            .Help("Povolí maximálně 1 požadavek za 10 sekund");


        api.MapGet("/demo/broken", async (ProductService service, ChaosContext chaos) =>
            {
                if (chaos.IsApiBroken())
                {
                    await Task.Delay(Random.Shared.Next(500, 1000));
                    return Results.StatusCode(500);
                }

                return Results.Ok(await service.GetProducts());
            })
            .Help("Rozbité API, všechny požadavky selžou");



        return api;
    }
}


public static class OasExt
{
    public static IEndpointConventionBuilder Help(this IEndpointConventionBuilder builder, string help)
    {
        return builder.WithOpenApi(_ => new (){OperationId = help});
    }
}