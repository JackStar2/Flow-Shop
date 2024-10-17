using OOP_cuoi_ki;
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
            // Create product list
            var products = new List<Product>
            {
                new Product(1, "Sản phẩm 1", "Nhà sản xuất 1", "Danh mục 1", DateTime.Parse("2024-01-01"), 100, 100000),
                new Product(2, "Sản phẩm 2", "Nhà sản xuất 2", "Danh mục 2", DateTime.Parse("2024-01-01"), 200, 200000)
            };

            // Create customer list
            var customers = new List<Customer>
            {
                new Customer("KH001", "123456", "Khách hàng 1", "0123456789", "Địa chỉ 1", "Thành viên"),
                new Customer("KH002", "123456", "Khách hàng 2", "0123456789", "Địa chỉ 2", "Thành viên")
            };

            // Create transfer list
            var transfers = new List<Transfer>
            {
                new Transfer(1, DateTime.Parse("2024-01-01"), 1, "Tiền mặt", 100000, "KH001", new List<Product> { products[0] }),
                new Transfer(2, DateTime.Parse("2024-01-01"), 2, "Thẻ tín dụng", 200000, "KH002", new List<Product> { products[1] })
            };

            // Create staff list
            var staffs = new List<Staff>
            {
                new Staff("001", "staff1", "Thien", "0123123123", "Quan 7", "Ca 1"),
                new Staff("002", "staff2", "BVao", "099999999", "BD", "Ca 2")
            };

            // Save data to JSON files
            SaveDataToJson(products, customers, transfers, staffs);

            // Load data from JSON files
            LoadDataFromJson();
            Console.ReadKey();
        }

        static void SaveDataToJson(List<Product> products, List<Customer> customers, List<Transfer> transfers, List<Staff> staffs)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            try
            {
                File.WriteAllText("products.json", JsonSerializer.Serialize(products, options));
                File.WriteAllText("customers.json", JsonSerializer.Serialize(customers, options));
                File.WriteAllText("transfers.json", JsonSerializer.Serialize(transfers, options));
                File.WriteAllText("staffs.json", JsonSerializer.Serialize(staffs, options));

                Console.WriteLine("Dữ liệu đã được lưu vào file JSON.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu dữ liệu: {ex.Message}");
            }
        }

        static void LoadDataFromJson()
        {
            if (File.Exists("products.json") && File.Exists("customers.json") &&
                File.Exists("transfers.json") && File.Exists("staffs.json"))
            {
                try
                {
                    var products = JsonSerializer.Deserialize<List<Product>>(File.ReadAllText("products.json"));
                    var customers = JsonSerializer.Deserialize<List<Customer>>(File.ReadAllText("customers.json"));
                    var transfers = JsonSerializer.Deserialize<List<Transfer>>(File.ReadAllText("transfers.json"));
                    var staffs = JsonSerializer.Deserialize<List<Staff>>(File.ReadAllText("staffs.json"));

                    DisplayData(products, customers, transfers, staffs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi đọc dữ liệu: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("File JSON không tồn tại.");
            }

            Console.ReadKey();
        }

        static void DisplayData(List<Product> products, List<Customer> customers, List<Transfer> transfers, List<Staff> staffs)
        {
            Console.WriteLine("Danh sách sản phẩm:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Tên sản phẩm: {product.ProductName}, Giá: {product.ProductPrice}");
            }

            Console.WriteLine("Danh sách khách hàng:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.User_id}, Tên khách hàng: {customer.User_name}, Địa chỉ: {customer.User_Address}");
            }

            Console.WriteLine("Danh sách hóa đơn:");
            foreach (var transfer in transfers)
            {
                Console.WriteLine($"ID: {transfer.TransferID}, Ngày lập: {transfer.Date}, Tổng tiền: {transfer.Total_Price()}");
            }

            Console.WriteLine("Danh sách nhân viên:");
            foreach (var staff in staffs)
            {
                Console.WriteLine($"ID: {staff.User_id}, Tên nhân viên: {staff.User_name}, Địa chỉ: {staff.User_Address}");
            }

        }
    }
}
