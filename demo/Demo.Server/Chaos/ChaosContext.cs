namespace Demo.Server.Chaos;

public class ChaosContext(IConfiguration config)
{
    public int RequestCounter { get; set; } = 0;
    public DateTime LastExecution { get; set; } = new ();

    /// <summary>
    /// 2/3 požadavků trvají 5 sekund
    /// </summary>
    public void SimulateSlowApi()
    {
        if (RequestCounter % 3 != 0)
        {
            Thread.Sleep(5000);
        }
    }

    /// <summary>
    /// 2/3 požadavků selžou
    /// </summary>
    public void SimulateFailingApi()
    {
        if (RequestCounter % 3 != 0)
        {
            throw new Exception("Simulated exception, Failing API");
        }
    }

    public bool IsApiBroken()
    {
        bool.TryParse(config["IsApiBroken"], out var isBroken);
        return isBroken;
    }

    public void SimulateTimeFailingApi()
    {
        if (DateTime.Now.Second > 10)
        {
            throw new Exception("Simulated exception, Time Failing");
        }
    }

    public bool IsRateLimitExceeded(TimeSpan timespan)
    {
        if (LastExecution.Add(timespan) <= DateTime.UtcNow)
        {
            LastExecution = DateTime.UtcNow;
            return false;
        }

        return true;
    }
}