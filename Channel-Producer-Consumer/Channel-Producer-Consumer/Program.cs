using Channel_Producer_Consumer;
using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSingleton<Channel<WorkTask>>( _ =>
Channel.CreateBounded<WorkTask>(new BoundedChannelOptions(100)
{
    FullMode = BoundedChannelFullMode.Wait,
    SingleReader = true, // Optimization if only 1 BackgroundService reads it
    SingleWriter = false // Allows multiple concurrent API controller requests/writes
}));

builder.Services.AddHostedService<ChannelConsumerService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();
app.Run();
