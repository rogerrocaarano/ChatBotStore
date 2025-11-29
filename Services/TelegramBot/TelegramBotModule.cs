using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using RestSharp.Serializers.Json;

namespace TelegramBot;

public static class TelegramBotModule
{
    internal static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private static RestClient BuildTelegramRestClient(string token)
    {
        var options = new RestClientOptions
        {
            BaseUrl = new Uri($"https://api.telegram.org/bot{token}/"),
        };

        var client = new RestClient(
            options,
            configureSerialization: s =>
                s.UseSerializer(() => new SystemTextJsonSerializer(SerializerOptions))
        );

        return client;
    }

    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var botToken = configuration["Telegram:Token"];
        if (string.IsNullOrEmpty(botToken))
        {
            throw new InvalidOperationException("Telegram:Token is not configured.");
        }

        services.AddSingleton<ITelegramBotPort>(sp =>
        {
            var restClient = BuildTelegramRestClient(botToken);
            return new TelegramBotAdapter(restClient);
        });

        return services;
    }
}
