using Common.Abstractions.Entities;
using Common.Types;

namespace Orders.Entities;

public class OrderItem : BaseEntity<Guid>
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public bool Removed { get; private set; }

    private OrderItem() { } // For ORMs

    private OrderItem(Guid id, Guid productId, int quantity, Money unitPrice)
        : base(id)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Removed = false;
    }

    public static OrderItem Create(Guid productId, Money unitPrice) =>
        new OrderItem(Guid.NewGuid(), productId, 1, unitPrice);

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity cannot be negative.");
        }

        Apply(()=> Quantity = quantity);
    }

    public void Remove()
    {
        Apply(() => Removed = true);
    }

    public Money TotalAmount()
    {
        return UnitPrice * Quantity;
    }
}
