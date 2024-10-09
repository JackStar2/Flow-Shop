using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
[Serializable]
public class Storage
{
    public List<Product> Products { get; set; } = new List<Product>();
    public Storage() { }
    public void AddProduct(Product product)
    {
        Product existingProduct = FindProductByID(product.ProductId);
        if (existingProduct != null)
        {
            existingProduct.UpdateStock(existingProduct.ProductQuantity + product.ProductQuantity);
        }
        else
        {
            Products.Add(product);
        }
        UpdateStock();
    }
    public Product FindProductByID(int id)
    {
        return Products.Find(p => p.ProductId == id);
    }
    public void UpdateStock()
    {
        Console.WriteLine("Stock updated for all products.");
    }
    public void CheckExpiredProduct()
    {
        foreach (Product product in Products)
        {
            if (product.Expired())
            {
                Console.WriteLine($"Product {product.ProductName} is expired.");
            }
        }
    }
    public bool CheckStock()
    {
        foreach (Product product in Products)
        {
            if (!product.CheckStock())
            {
                Console.WriteLine($"Product {product.ProductName} is out of stock.");
                return false;
            }
        }
        return true;
    }
    public void ReduceStock(int productId, int amount)
    {
        Product product = FindProductByID(productId);
        if (product != null)
        {
            product.ReduceStock(amount);
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }    public void SerializeToJson(string filePath)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string jsonString = JsonSerializer.Serialize(Products, options);
        File.WriteAllText(filePath, jsonString);
        Console.WriteLine("Storage serialized to JSON.");
    }
    public void DeserializeFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            Products = JsonSerializer.Deserialize<List<Product>>(jsonString);
            Console.WriteLine("Storage deserialized from JSON.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}
