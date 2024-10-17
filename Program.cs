using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BanHang
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Khởi tạo danh sách khách hàng
            list_Customer customerList = new list_Customer();
            customerList.Add(new Customer("001", "123", "Lương", "0000000", "quan 8", "Diamond"));
            customerList.Add(new Customer("002", "123", "Thiện", "0000000", "quan 7", "Bronze"));

            // Ghi dữ liệu ra file JSON
            string fileCustomer = "customerList.json";
            string jsonCustomer = JsonSerializer.Serialize(customerList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileCustomer, jsonCustomer);

            // Đọc lại dữ liệu từ file JSON
            string newJsonCustomer = File.ReadAllText(fileCustomer);
            list_Customer deserializedCustomerList = JsonSerializer.Deserialize<list_Customer>(newJsonCustomer);

            // Hiển thị dữ liệu với giao diện đẹp
            Console.WriteLine("Danh sách khách hàng:");
            Console.WriteLine(new string('-', 60));  // Dòng phân cách
            Console.WriteLine($"{"ID",-10}{"Tên",-15}{"SĐT",-15}{"Địa chỉ",-10}{"Thành viên",-10}");
            Console.WriteLine(new string('-', 60));  // Dòng phân cách

            // Kiểm tra và hiển thị danh sách khách hàng
            if (deserializedCustomerList != null && deserializedCustomerList.Customers.Count > 0)
            {
                foreach (var customer in deserializedCustomerList.Customers)
                {
                    Console.WriteLine($"{customer.User_id,-10}{customer.User_name,-15}{customer.User_Phone,-15}{customer.User_Address,-10}{customer.Membership,-10}");
                }
            }
            else
            {
                Console.WriteLine("Danh sách khách hàng trống.");
            }

            Console.WriteLine(new string('-', 60));  // Dòng phân cách
        }
    }
}
