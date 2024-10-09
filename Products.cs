using System.Runtime.Serialization;
using System;

[Serializable]
public abstract class Product : ISerializable
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductManufacturer { get; set; }
    public string ProductCategory { get; set; }
    public bool ProductExpired { get; set; }
    public int ProductQuantity { get; set; }
    public int ProductPrice { get; set; }

    protected Product(SerializationInfo info, StreamingContext context)
    {
        ProductId = info.GetInt32(nameof(ProductId));
        ProductName = info.GetString(nameof(ProductName));
        ProductManufacturer = info.GetString(nameof(ProductManufacturer));
        ProductCategory = info.GetString(nameof(ProductCategory));
        ProductExpired = info.GetBoolean(nameof(ProductExpired));
        ProductQuantity = info.GetInt32(nameof(ProductQuantity));
        ProductPrice = info.GetInt32(nameof(ProductPrice));
    }

    public Product() { }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(ProductId), ProductId);
        info.AddValue(nameof(ProductName), ProductName);
        info.AddValue(nameof(ProductManufacturer), ProductManufacturer);
        info.AddValue(nameof(ProductCategory), ProductCategory);
        info.AddValue(nameof(ProductExpired), ProductExpired);
        info.AddValue(nameof(ProductQuantity), ProductQuantity);
        info.AddValue(nameof(ProductPrice), ProductPrice);
    }

    public abstract void ShowInfo();
    public abstract bool CheckStock();
    public abstract void ReduceStock();
    public abstract bool Expired();
    public abstract void UpdatePrice(int newPrice);
    public abstract void UpdateStock(int newQuantity);

    internal void ReduceStock(int amount)
    {
        throw new NotImplementedException();
    }
}