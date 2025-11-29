using System.Text.Json.Nodes;
using LiteBus.Commands.Abstractions;

namespace TelegramBot.Commands;

public record ReceiveTelegramUpdateCommand(JsonObject Update) : ICommand;
