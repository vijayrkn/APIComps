
using System.Diagnostics;

public class Program
{
    private const int NumberofServers = 3;
    private const int MaxNumber = 90;
    private const string BaseURL = "http://localhost:5000/compute";
    private const string ResponseHeader = "stack";
    private static HttpClient _client = new HttpClient();

    public async static Task Main()
    {
        IDictionary<string, int> wins = new Dictionary<string, int>();
        int counter = 0;

        var timer = new Stopwatch();
        while (counter < MaxNumber)
        {
            long winningTime = int.MaxValue;
            string winningStack = "undefined";

            for (int i = 0; i < NumberofServers; i++)
            {
                timer.Restart();
                var response = await _client.GetAsync(@$"{BaseURL}?n={counter}");
                timer.Stop();
                var responseTime = timer.ElapsedTicks;

                if (!response.IsSuccessStatusCode && !response.Headers.TryGetValues(ResponseHeader, out var _))
                {
                    return;
                }

                // Get the stack header from response.
                string? currentStack = response.Headers.GetValues(ResponseHeader).FirstOrDefault();
                if(currentStack == null)
                {
                    return;
                }

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

        Console.WriteLine("Final Result:");
        foreach(var win in wins)
        {
            Console.WriteLine($"{win.Key} : {win.Value}");
        }
    }
}
