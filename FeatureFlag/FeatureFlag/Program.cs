using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);
//Source : https://www.c-sharpcorner.com/article/feature-flags-in-asp-net-core-11-safe-feature-rollouts-and-progressive-deployme/
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddFeatureManagement();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


app.MapGet("/featureflag", async (IFeatureManager featureManager) =>
{
    if (await featureManager.IsEnabledAsync("BetaFeature"))
    {
        return Results.Ok("Beta Feature is enabled");
    }
    else
    {
        return Results.Ok("Beta Feature is disabled");
    }
});

// InController we can use FeatureGate attribute to check if the feature is enabled or not.
// If the feature is disabled, the controller action will not be executed and a 404 Not Found response will be returned.
//If NewCheckout==false then it will return 404 Not Found when you try to access /api/checkout endpoint.
////If NewCheckout==true then it will return "New Checkout" when you try to access /api/checkout endpoint.
/*
    [FeatureGate("NewCheckout")]
    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("New Checkout");
        }
    } 
*/

//Gradually expose a feature to a percentage of users
/*
   "FeatureManagement": {
    "NewCheckout": {
      "EnabledFor": [
        {
          "Name": "Percentage",
          "Parameters": {
            "Value": 20
          }
        }
      ]
    }
  }
 
 */

//Time based activation
/*
 "FeatureManagement": {
    "HolidaySale": {
      "EnabledFor": [
        {
          "Name": "TimeWindow",
          "Parameters": {
            "Start": "2026-12-01T00:00:00Z",
            "End": "2026-12-31T23:59:59Z"
          }
        }
      ]
    }
  }
 */
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
