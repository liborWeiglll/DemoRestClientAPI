using Demo.Client;
using Demo.Client.Examples;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

ServiceCollection services = new ServiceCollection();
services.AddHttpClient();
services.AddLogging(x => x.AddConsole());
services.AddSingleton<RetryHandler>();
services.AddTransient<App>();

// services.Demo01_Basic();
//services.Demo02_Handlers();
// services.Demo03_Retry();
// services.Demo04_CircuitBreaker();
// services.Demo05_Fallback();
// services.Demo06_Timeout();
// services.Demo07_Hedging();
// services.Demo08_RateLimiter();

var serviceProvider = services.BuildServiceProvider();


// var app = serviceProvider.GetRequiredService<App>();
//
// int executions = 10;
// for (int i = 1; i <= executions; i++)
// {
//  await app.Run();
// }

await serviceProvider.Demo09_PollyImpl();