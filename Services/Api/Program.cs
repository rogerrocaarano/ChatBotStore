using System.Text.Json;
using Common;
using FastEndpoints;
using FastEndpoints.Swagger;
using TelegramBot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTelegramBot(builder.Configuration);

var featureAssemblies = new[]
{
    typeof(TelegramBotModule).Assembly,
};

builder.Services.AddApplication(featureAssemblies);

builder.Services.AddFastEndpoints().SwaggerDocument();

var app = builder.Build();
app.UseFastEndpoints(config =>
    {
        config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    })
    .UseSwaggerGen();

app.Run();
