var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/compute", (ulong n, HttpResponse response) =>
{
    ulong a = 0, b = 1, c = 0, result = 0;
    if (n == 0)
    {
        result = a;
    }
    else
    {
        for (ulong i = 2; i <= n; i++)
        {
            c = a + b;
            a = b;
            b = c;
        }
        result = b;
    }

    response.Headers["stack"] = "dotnet";
    return Results.Json(new { Result = result });
});

app.Run();