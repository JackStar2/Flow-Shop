﻿[Serializable]
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

    public void ShowInfo()
    {
        Console.WriteLine($"Product ID: {ProductId}");
        Console.WriteLine($"Product Name: {ProductName}");
        Console.WriteLine($"Product Expired: {ProductExpired}");
        Console.WriteLine($"Product Quantity: {ProductQuantity}");
        Console.WriteLine($"Product Price: {ProductPrice}");
    }

    public bool CheckStock()
    {
        return ProductQuantity > 0;
    }

    public void ReduceStock()
    {
        if (ProductQuantity > 0)
        {
            ProductQuantity--;
        }
        else
        {
            Console.WriteLine("Out of stock!");
        }
    }

    public bool Expired()
    {
        return ProductExpired;
    }

    public void UpdatePrice(int newPrice)
    {
        ProductPrice = newPrice;
    }

    public void UpdateStock(int newQuantity)
    {
        ProductQuantity = newQuantity;
    }
}