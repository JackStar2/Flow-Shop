using System.Runtime.Serialization;
using System;

[Serializable]
public class Product : ISerializable
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductManufacturer { get; set; }
    public string ProductCategory { get; set; }
    public DateTime ProductExpired { get; set; }
    public int ProductQuantity { get; set; }
    public int ProductPrice { get; set; }

    public Product(int productId, string productName, string productManufacturer, string productCategory, DateTime productExpired, int productQuantity, int productPrice)
    {
        ProductId = productId;
        ProductName = productName;
        ProductManufacturer = productManufacturer;
        ProductCategory = productCategory;
        ProductExpired = productExpired;
        ProductQuantity = productQuantity;
        ProductPrice = productPrice;
    }

    protected Product(SerializationInfo info, StreamingContext context)
    {
        ProductId = info.GetInt32("ProductId");
        ProductName = info.GetString("ProductName");
        ProductManufacturer = info.GetString("ProductManufacturer");
        ProductCategory = info.GetString("ProductCategory");
        ProductExpired = info.GetDateTime("ProductExpired");
        ProductQuantity = info.GetInt32("ProductQuantity");
        ProductPrice = info.GetInt32("ProductPrice");
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("ProductId", ProductId);
        info.AddValue("ProductName", ProductName);
        info.AddValue("ProductManufacturer", ProductManufacturer);
        info.AddValue("ProductCategory", ProductCategory);
        info.AddValue("ProductExpired", ProductExpired);
        info.AddValue("ProductQuantity", ProductQuantity);
        info.AddValue("ProductPrice", ProductPrice);
    }

    public void ShowInfo(Product product)
    {
        Console.WriteLine($"Product ID: {product.ProductId}");
        Console.WriteLine($"Product Name: {product.ProductName}");
        Console.WriteLine($"Product Expired: {product.ProductExpired}");
        Console.WriteLine($"Product Quantity: {product.ProductQuantity}");
        Console.WriteLine($"Product Price: {product.ProductPrice}");
    }

    public bool CheckStock()
    {
        return ProductQuantity > 0;
    }

    public void ReduceStock(int amount)
    {
        if (ProductQuantity > 0)
        {
            ProductQuantity -= amount;
        }
        else
        {
            Console.WriteLine("Out of stock!");
        }
    }

    public bool IsExpired()
    {
        return DateTime.Now > ProductExpired;
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
