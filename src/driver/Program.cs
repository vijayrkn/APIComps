
using System.Diagnostics;

public class Program
{
    private const int NumberofServers = 4;
    private const int MaxNumber = 50;
    private const string BaseURL = "http://localhost:5000";
    private const string StackHeader = "stack";
    private static HttpClient _client = new();
    private static Stopwatch _timer = new();

    public static async Task Main()
    {
        IDictionary<string, int> wins = new Dictionary<string, int>();
        int counter = 0;

        while (counter < MaxNumber)
        {
            long winningTime = int.MaxValue;
            string winningStack = "undefined";

            for (int i = 0; i < NumberofServers; i++)
            {
                (HttpResponseMessage response, long responseTime) = await ProcessComputeRequest(counter);
                if (!response.IsSuccessStatusCode && !response.Headers.TryGetValues(StackHeader, out var responseStackValue)
                    && responseStackValue?.FirstOrDefault() == null)
                {
                    return;
                }

                string? currentStack = response.Headers.GetValues(StackHeader).First();
                if (responseTime < winningTime)
                {
                    winningTime = responseTime;
                    winningStack = currentStack;
                }
            }

            if (!wins.ContainsKey(winningStack))
            {
                wins[winningStack] = 0;
            }
            wins[winningStack]++;
            counter++;
        }

        Console.WriteLine("## Result:");
        foreach(var win in wins)
        {
            Console.WriteLine($"{win.Key} : {win.Value}");
        }
    }

    private static async Task<(HttpResponseMessage, long)> ProcessComputeRequest(int counter)
    {
        _timer.Restart();
        var response = await _client.GetAsync(@$"{BaseURL}/compute?n={counter}");
        _timer.Stop();
        var responseTime = _timer.ElapsedTicks;

        return (response, responseTime);
    }
}
