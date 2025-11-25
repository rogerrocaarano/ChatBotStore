using Common.Abstractions.Entities;
using Common.Types;

namespace Orders.Entities;

public class Order : BaseEntity<Guid>, IAggregateRoot
{
    public Guid CustomerId { get; private set; }
    public IReadOnlyCollection<OrderItem> Cart => _cart.AsReadOnly();
    private List<OrderItem> _cart = [];
    public LocationPoint? ClientLocation { get; private set; }
    public OrderStatus Status { get; private set; }
    public Guid? PaymentId { get; private set; }
    public Guid? RiderId { get; private set; }
    public Money DeliveryFee { get; private set; }
    public Currency Currency { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Order() { } // For ORMs
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Order(
        Guid id,
        Guid customerId,
        List<OrderItem> cart,
        LocationPoint? clientLocation,
        OrderStatus status,
        Guid? paymentId,
        Guid? riderId,
        Money deliveryFee,
        Currency currency
    )
        : base(id)
    {
        CustomerId = customerId;
        _cart = cart;
        ClientLocation = clientLocation;
        Status = status;
        PaymentId = paymentId;
        RiderId = riderId;
        DeliveryFee = deliveryFee;
        Currency = currency;
    }

    public static Order Create(Guid customerId, Currency currency)
    {
        return new Order(
            id: Guid.NewGuid(),
            customerId: customerId,
            cart: [],
            clientLocation: null,
            status: OrderStatus.CustomerPending,
            paymentId: null,
            riderId: null,
            deliveryFee: new Money(0, currency),
            currency: currency
        );
    }

    public void AddItem(OrderItem item)
    {
        if (Status != OrderStatus.CustomerPending)
        {
            throw new InvalidOperationException("Cannot modify the cart when the order is not pending.");
        }

        if (item.UnitPrice.Currency != Currency)
        {
            throw new InvalidOperationException("Item currency does not match order currency.");
        }

        var existingItem = _cart.Find(i => i.ProductId == item.ProductId);
        if (existingItem != null)
        {
            Apply(()=> existingItem.UpdateQuantity(existingItem.Quantity + item.Quantity));
        }
        else
        {
            Apply(()=> _cart.Add(item));
        }
    }

    public void RemoveItem(Guid productId)
    {
        if (Status != OrderStatus.CustomerPending)
        {
            throw new InvalidOperationException("Cannot modify the cart when the order is not pending.");
        }

        var existingItem = _cart.Find(i => i.ProductId == productId);
        if (existingItem != null)
        {
            Apply(() => _cart.Remove(existingItem));
        }
    }

    public void UpdateItemQuantity(Guid productId, int quantity)
    {
        if (Status != OrderStatus.CustomerPending)
        {
            throw new InvalidOperationException("Cannot modify the cart when the order is not pending.");
        }

        var existingItem = _cart.Find(i => i.ProductId == productId);
        if (existingItem != null)
        {
            Apply(() => existingItem.UpdateQuantity(quantity));
        }
    }

    public void SetShippingAddress(LocationPoint location)
    {
        if (Status is not (OrderStatus.CustomerPending or OrderStatus.CustomerConfirmed))
        {
            throw new InvalidOperationException("Cannot set location when the order is not pending or confirmed.");
        }
        Apply(() =>
        {
            ClientLocation = location;
            CalculateDeliveryFee();
            Status = OrderStatus.CustomerConfirmed;
        });
    }

    private void CalculateDeliveryFee()
    {
        // TODO: Add more complex logic based on distance, time, etc.
        var baseFee = new Money(5.0m, Currency);
        DeliveryFee = baseFee;
    }

    public Money TotalAmount()
    {
        var itemsTotal = new Money(0m, Currency);
        itemsTotal = _cart.Aggregate(itemsTotal, (current, item) => current + item.TotalAmount());
        return itemsTotal + DeliveryFee;
    }

    public void CustomerPaid(Guid paymentId)
    {
        if (Status != OrderStatus.CustomerConfirmed)
            throw new InvalidOperationException("Order must be confirmed by the customer before payment.");
        Apply(() =>
        {
            Status = OrderStatus.CustomerPaid;
            PaymentId = paymentId;
        });
    }

    public void PrepareOrder()
    {
        if (Status != OrderStatus.CustomerPaid)
            throw new InvalidOperationException("Order must be paid before preparation.");
        Apply(() => Status = OrderStatus.InPreparation);
    }

    public void FinishPreparation()
    {
        if (Status != OrderStatus.InPreparation)
            throw new InvalidOperationException("Order must be in preparation to be marked as ready for pickup.");
        Apply(() => Status = OrderStatus.ReadyForPickup);
    }

    public void AssignRider(Guid riderId)
    {
        if (Status != OrderStatus.ReadyForPickup)
            throw new InvalidOperationException("Order must be ready for pickup to assign a rider.");
        Apply(() =>
        {
            RiderId = riderId;
            Status = OrderStatus.WaitingForRider;
        });
    }

    public void PickupByRider()
    {
        if (Status != OrderStatus.WaitingForRider)
            throw new InvalidOperationException("Order must be waiting for rider to be picked up.");
        Apply(() => Status = OrderStatus.OnTheWay);
    }

    public void ArriveAtCustomerLocation()
    {
        if (Status != OrderStatus.OnTheWay)
            throw new InvalidOperationException("Order must be on the way to arrive at customer location.");
        Apply(() => Status = OrderStatus.AtCustomerLocation);
    }

    public void Complete()
    {
        if (Status is not (OrderStatus.ReadyForPickup or OrderStatus.AtCustomerLocation))
            throw new InvalidOperationException("Order must be ready for pickup or at customer location to be completed.");
        Apply(() => Status = OrderStatus.Completed);
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Completed or cancelled orders cannot be cancelled.");
        Apply(() => Status = OrderStatus.Cancelled);
    }
}


public enum OrderStatus
{
    CustomerPending,
    CustomerConfirmed,
    CustomerPaid,
    InPreparation,
    ReadyForPickup,
    WaitingForRider,
    OnTheWay,
    AtCustomerLocation,
    Completed,
    Cancelled,
}

