using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

using System;
using System.Runtime.Serialization;

[Serializable]
public abstract class User : ISerializable
{
    public string User_id { get; set; }
    public string User_Password { get; set; }
    public string User_name { get; set; }
    public string User_Phone { get; set; }
    public string User_Address { get; set; }

    public User(string id, string password, string name, string phone, string address)
    {
        User_id = id;
        User_Password = password;
        User_name = name;
        User_Phone = phone;
        User_Address = address;
    }

    protected User(SerializationInfo info, StreamingContext context)
    {
        User_id = info.GetString("User_id");
        User_Password = info.GetString("User_Password");
        User_name = info.GetString("User_name");
        User_Phone = info.GetString("User_Phone");
        User_Address = info.GetString("User_Address");
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("User_id", User_id);
        info.AddValue("User_Password", User_Password);
        info.AddValue("User_name", User_name);
        info.AddValue("User_Phone", User_Phone);
        info.AddValue("User_Address", User_Address);
    }

    public abstract void ShowInfo();
}

[Serializable]
class Customer : User
{
    private static List<Customer> customers = new List<Customer>();
    private List<string> purchaseHistory = new List<string>(); // Lưu trữ lịch sử mua hàng

    public string Membership { get; set; }
    public int LoyaltyPoints { get; set; }

    public Customer(string user_id, string user_password, string user_name, string user_phone, string user_address, string membership)
        : base(user_id, user_password, user_name, user_phone, user_address)
    {
        Membership = membership;
        LoyaltyPoints = 0;
    }
    protected Customer(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Membership = info.GetString("Membership");
        LoyaltyPoints = info.GetInt32("LoyaltyPoints");
    }
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("Membership", Membership);
        info.AddValue("LoyaltyPoints", LoyaltyPoints);
    }

    // Phương thức đăng ký khách hàng
    public void Register()
    {
        customers.Add(this); // Thêm khách hàng vào danh sách
        Console.WriteLine("Customer " + User_name + " registered successfully. Membership: " + Membership);
    }

    // Phương thức thêm giao dịch mua vào lịch sử
    public void AddPurchase(string productName, int quantity)
    {
        string purchaseRecord = "Product: " + productName + ", Quantity: " + quantity + ", Date: " + DateTime.Now;
        purchaseHistory.Add(purchaseRecord);
        Console.WriteLine("Purchase added: " + quantity + " of " + productName + " on " + DateTime.Now);
    }

    // Lấy lịch sử mua hàng
    public void GetPurchaseHistory()
    {
        if (purchaseHistory.Count == 0)
        {
            Console.WriteLine("No purchase history available.");
            return;
        }

        Console.WriteLine("Purchase history for customer " + User_name + ":");
        for (int i = 0; i < purchaseHistory.Count; i++)
        {
            string record = purchaseHistory[i];
            Console.WriteLine(record);
        }
    }
    public override void ShowInfo()
    {
        Console.WriteLine("Customer ID: " + User_id + ", Name: " + User_name + ", Membership: " + Membership + ", Points: " + LoyaltyPoints);
    }
}

[Serializable]
class Manager : User
{
    public Manager(string id, string password, string name, string phone, string address)
        : base(id, password, name, phone, address)
    {
    }

    // Constructor cho deserialization
    protected Manager(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    // Tuần tự hóa dữ liệu
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);  // Gọi phương thức của lớp cha
    }

    // Các phương thức dành cho Manager
    public void AddProductToStorage(string product)
    {
        Console.WriteLine($"Product {product} has been added to storage.");
    }

    public void UpdateStock(string product, int newQuantity)
    {
        Console.WriteLine($"Stock for {product} updated to {newQuantity} units.");
    }

    public void ReduceStock(string product, int quantity)
    {
        Console.WriteLine($"{quantity} units of {product} have been removed from stock.");
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"Manager ID: {User_id}, Name: {User_name}");
    }
}
[Serializable]
public class Staff : User
{
    public string StaffShift { get; set; }

    [JsonConstructor]
    public Staff(string user_id, string user_password, string user_name, string user_phone, string user_address, string staffShift)
        : base(user_id, user_password, user_name, user_phone, user_address)
    {
        StaffShift = staffShift;
    }

    protected Staff(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        StaffShift = info.GetString("StaffShift");
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("StaffShift", StaffShift);
    }

    public void LogIn(string enteredPassword)
    {
        if (enteredPassword == User_Password)
        {
            Console.WriteLine($"Staff {User_name} logged in for shift: {StaffShift}");
        }
        else
        {
            Console.WriteLine("Incorrect password. Login failed.");
        }
    }

    public void Invoice(string customerName, string product, int quantity)
    {
        Console.WriteLine($"Invoice generated by staff {User_name} for customer {customerName}:");
        Console.WriteLine($"Product: {product}, Quantity: {quantity}");
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"Staff ID: {User_id}, Name: {User_name}, Shift: {StaffShift}");
    }
}

