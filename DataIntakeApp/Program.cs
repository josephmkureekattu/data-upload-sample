using Azure.Identity;
using Core.Features.Commands.UploadFile;
using Core.IOC;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.IOC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
if (!builder.Environment.IsDevelopment())
{
    var keyVaultEndpoint = new Uri($"https://kv-jsp-free-trial.vault.azure.net/");
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
    builder.Services.AddApplicationInsightsTelemetry(options => { options.ConnectionString = "InstrumentationKey=4cb169fe-1800-4a23-846b-428b7c063c0b;IngestionEndpoint=https://centralus-2.in.applicationinsights.azure.com/;LiveEndpoint=https://centralus.livediagnostics.monitor.azure.com/"; });
}
builder.Services.RegisterCoreDependency();
builder.Services.RegisterPersistenceDependency(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

///app.UseHttpsRedirection();



var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapPost("main/uploadfile", async (HttpRequest request, IMediator mediator) =>
{
    UploadFileCommand uploadFileCommand = new UploadFileCommand
    {
        Files = request.Form.Files
    };
    var response = await mediator.Send(uploadFileCommand);

    return response;
})
.WithName("UploadFile");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}