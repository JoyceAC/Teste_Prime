// IEnumerable<WarehouseEntry> should be returned by GetProducts method.
// It can be mapped from ProductRecord
public class WarehouseEntry
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

// NotPositiveQuantityMessage should be returned by
// SetProductCapacity, ReceiveProduct and DispatchProduct methods
public class NotPositiveQuantityMessage
{
    public string Message => "A positive quantity was not provided";
}

// QuantityTooLowMessage should be returned by
// SetProductCapacity, ReceiveProduct and DispatchProduct methods
public class QuantityTooLowMessage
{
    public string Message => "Too low a quantity was provided";
}

// QuantityTooHighMessage should be returned by
// SetProductCapacity, ReceiveProduct and DispatchProduct methods
public class QuantityTooHighMessage
{
    public string Message => "Too high a quantity was provided";
}