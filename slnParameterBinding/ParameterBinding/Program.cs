using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ParameterBinding;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IGetDetails, GetDetails>();

builder.Services.AddAntiforgery();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapGet("getname/{name}", (string name,[FromServices]IGetDetails getDetails, [FromHeader(Name = "X-Custom-Header")] string customHeader) =>
 {
     var message = getDetails.GetName();
     return Results.Ok("Hell " +  name + " CustomeHead= " + customHeader);
 });

app.MapGet("/{id}", (HttpRequest request) =>
{
    var id = request.RouteValues["id"];
    var page = request.Query["page"];
    var customHeader = request.Headers["X-Custom-Header"];

    return Results.Ok();
});


app.MapPost("/todo",async ([FromForm] string name, [FromForm] string description, IFormFile attachement) =>
{
    var toDo = new ToDo
    {
        Name=name,
        Description = description
    };

    if (attachement != null)
    {
        var extension = Path.GetExtension(attachement.FileName);
        var attachmentName = Path.GetRandomFileName() + extension;


        using var stream =  File.Create(Path.Combine("c:\\Temp", attachmentName)); //File.Create(Path.Combine("wwwroot", attachmentName));
        await attachement.CopyToAsync(stream);
    
    }

    return Results.Ok();
}).DisableAntiforgery();


app.MapPost("/upload", async (IFormFile file) =>
{
    var tempFile = Path.GetTempFileName();
    app.Logger.LogInformation(tempFile);
    using var stream = File.OpenWrite(tempFile);
    await file.CopyToAsync(stream);
});

app.MapPost("/upload_many", async (IFormFileCollection myFiles) =>
{
    foreach (var file in myFiles)
    {
        var extension = Path.GetExtension(file.FileName);
        var tempFile = Path.GetRandomFileName() + extension;
        app.Logger.LogInformation(tempFile);
        
        using var stream = File.OpenWrite(Path.Combine("c:\\Temp", tempFile));
        await file.CopyToAsync(stream);
    }
}).DisableAntiforgery();




app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


class ToDo
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}