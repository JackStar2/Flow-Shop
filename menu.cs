using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
public class menu
{
    public static void init_customer()
    {
        string fileCustomer = "customerList.json";

        // Kiểm tra xem file JSON có tồn tại không
        list_Customer customerList;
            if (File.Exists(fileCustomer))
            {
                // Đọc dữ liệu từ file JSON nếu file tồn tại
                string jsonCustomer = File.ReadAllText(fileCustomer);
            customerList = JsonSerializer.Deserialize<list_Customer>(jsonCustomer) ?? new list_Customer();
            }
            else
            {
                // Nếu file không tồn tại, khởi tạo danh sách khách hàng mới
                customerList = new list_Customer();
            }

        Console.WriteLine("Nhập thông tin khách hàng:");
        Console.Write("ID: ");
        string id = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();
        Console.Write("Tên: ");
        string name = Console.ReadLine();
        Console.Write("Số điện thoại: ");
        string phone = Console.ReadLine();
        Console.Write("Địa chỉ: ");
        string address = Console.ReadLine();
        Console.Write("Hạng thành viên (Bronze, Silver, Gold, Diamond): ");
        string membership = Console.ReadLine();

        customerList.Add(new Customer(id, password, name, phone, address, membership));

        // Ghi lại toàn bộ danh sách khách hàng vào file JSON
        string newJsonCustomer = JsonSerializer.Serialize(customerList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileCustomer, newJsonCustomer);

        // Hiển thị danh sách khách hàng
        Console.WriteLine("Danh sách khách hàng:");
        Console.WriteLine(new string('-', 60));  // Dòng phân cách
        Console.WriteLine($"{"ID",-10}{"Tên",-15}{"SĐT",-15}{"Địa chỉ",-10}{"Thành viên",-10}");
        Console.WriteLine(new string('-', 60));  // Dòng phân cách

        if (customerList.Customers.Count > 0)
        {
            for (int i = 0; i < customerList.Customers.Count; i++)
            {
                Customer customer = customerList.Customers[i];
                Console.WriteLine($"{customer.User_id,-10}{customer.User_name,-15}{customer.User_Phone,-15}{customer.User_Address,-10}{customer.Membership,-10}");
            }
        }
        else
        {
            Console.WriteLine("Danh sách khách hàng trống.");
        }
    }

}
