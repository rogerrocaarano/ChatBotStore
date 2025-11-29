using Common.Types;
using LiteBus.Queries.Abstractions;

namespace Customers.Queries;

public record GetCustomerIdFromTelegramQuery(Telegram Telegram) : IQuery<Guid>;
