using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/json-item", (CancellationToken cancellationToken) =>
    {


        return TypedResults.ServerSentEvents(GetHeartRate(cancellationToken), "text/event-stream");
    });

app.Run();


async IAsyncEnumerable<HeartRateRecord> GetHeartRate([EnumeratorCancellation] CancellationToken cancellationToken)
{
    while (!cancellationToken.IsCancellationRequested)
    {
        var hearRate = Random.Shared.Next(60, 100);
        yield return new HeartRateRecord(hearRate);
        await Task.Delay(1000, cancellationToken);
    }
}

public record HeartRateRecord(int HeartRate);
