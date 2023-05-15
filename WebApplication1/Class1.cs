using System;
using System.Collections.Generic;
public interface IWarehouseRepository
{
    void SetCapacityRecord(int productId, int capacity);
    IEnumerable<CapacityRecord> GetCapacityRecords();
    IEnumerable<CapacityRecord> GetCapacityRecords(Func<CapacityRecord, bool> filter);

    void SetProductRecord(int productId, int quantity);
    IEnumerable<ProductRecord> GetProductRecords();
    IEnumerable<ProductRecord> GetProductRecords(Func<ProductRecord, bool> filter);
}

public class CapacityRecord
{
    public int ProductId { get; set; }
    public int Capacity { get; set; }
}

public class ProductRecord
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}