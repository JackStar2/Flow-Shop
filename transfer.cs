using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

[Serializable]
public class Transfer : ISerializable
{
    public int TransferID { get; set; }
    public DateTime Date { get; set; }
    public int CashierID { get; set; }
    public string PaymentMethod { get; set; }
    public decimal CustomerMoney { get; set; }
    public string CustomerID { get; set; }
    public List<Product> ListItem { get; set; } = new List<Product>();

    // Constructor for creating a new Transfer
    public Transfer(int transferID, DateTime date, int cashierID, string paymentMethod, decimal customerMoney, string customerID, List<Product> listItem)
    {
        TransferID = transferID;
        Date = date;
        CashierID = cashierID;
        PaymentMethod = paymentMethod;
        CustomerMoney = customerMoney;
        CustomerID = customerID;
        ListItem = listItem;
    }

    // Constructor for deserialization
    protected Transfer(SerializationInfo info, StreamingContext context)
    {
        TransferID = info.GetInt32("TransferID");
        Date = info.GetDateTime("Date");
        CashierID = info.GetInt32("CashierID");
        PaymentMethod = info.GetString("PaymentMethod");
        CustomerMoney = info.GetDecimal("CustomerMoney");
        CustomerID = info.GetString("CustomerID");

        // Deserialize ListItem
        ListItem = (List<Product>)info.GetValue("ListItem", typeof(List<Product>));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("TransferID", TransferID);
        info.AddValue("Date", Date);
        info.AddValue("CashierID", CashierID);
        info.AddValue("PaymentMethod", PaymentMethod);
        info.AddValue("CustomerMoney", CustomerMoney);
        info.AddValue("CustomerID", CustomerID);

        // Serialize ListItem
        info.AddValue("ListItem", ListItem);
    }

    public decimal Total_Price()
    {
        decimal total = 0;
        foreach (var item in ListItem)
        {
            total += item.ProductPrice * item.ProductQuantity;
        }
        return total;
    }

    public decimal Exchange()
    {
        decimal total = Total_Price();
        decimal exchange = CustomerMoney - total;

        if (exchange < 0)
        {
            Console.WriteLine("Not enough money to complete the payment.");
            return 0;
        }

        return exchange;
    }

    public void Add_Item(Product product)
    {
        ListItem.Add(product);
    }

    public void Remove_Item(Product product)
    {
        ListItem.Remove(product);
    }
}
