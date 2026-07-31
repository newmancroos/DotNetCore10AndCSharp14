using JasperFx.CodeGeneration;
using prjWolverine;
using System.Reflection;
using Wolverine;
using Wolverine.RabbitMQ;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Run dotnet run -- codegen write abd then
//dotnet add package WolverineFx.RuntimeCompilation  -- Worked


//builder.Host.UseWolverine(); //If we only use Wolverine for domain events we don't need to use any options

//If we use RabbitMq then we need to configure it
builder.Host.UseWolverine(options =>
{
    options.UseRabbitMq("amqp://guest:guest@localhost:5672/")
            .AutoProvision()  // This will create reqquired Topics and Queue as MassTransit does
            .UseConventionalRouting();  //Route the message according to the message type
});


//builder.Services.AddWolverineHttp();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/GETEmployees/{employeeId:int}", async (int employeeId , IMessageBus bus) =>
{
    GetEmployeeRequest request = new GetEmployeeRequest(employeeId);
    GetEmployeeResponse response = await bus.InvokeAsync<GetEmployeeResponse>(request);
    return Results.Ok(response);
}).WithName("GETEmployees");

app.UseHttpsRedirection();
//app.MapWolverineEndpoints();



app.Run();

//return await app.RunJasperFxCommands(args);
