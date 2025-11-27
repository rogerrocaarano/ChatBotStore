using System.ComponentModel;

namespace Common.Types;

public enum Currency
{
    [Description("Bolivianos")]
    BOB,

    [Description("Dólares Estadounidenses")]
    USD,

    [Description("Libras Esterlinas")]
    GBP,

    [Description("Euros")]
    EUR,
}
