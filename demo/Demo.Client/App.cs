using Demo.Contracts;

namespace Demo.Client;

public class App(ApiClient client)
{
    public async Task Run()
    {
        CancellationTokenSource cts = new();
        // cts.CancelAfter(1000);

        ApiResponse<List<ProductDto>> result = await client.GetProducts(cts.Token);

        if (result.IsSuccess)
        {
            foreach (var item in result.Data)
            {
                Console.WriteLine(item.ToString());
            }

            if (result.Data.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Žádné položky nebyly vráceny.");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + result.Error);
        }

    }
}