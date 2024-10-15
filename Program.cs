using OOP_cuoi_ki;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BanHang
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true
            };
            // Tạo một danh sách sản phẩm
            List<Product> products = new List<Product>();

            // Tạo một sản phẩm mới
            Product product1 = new Product(1, "Sản phẩm 1", "Nhà sản xuất 1", "Danh mục 1", DateTime.Parse("2024-01-01"), 100, 100000);
            Product product2 = new Product(2, "Sản phẩm 2", "Nhà sản xuất 2", "Danh mục 2", DateTime.Parse("2024-01-01"), 200, 200000);

            string json = JsonSerializer.Serialize(products, options);
            products = JsonSerializer.Deserialize<List<Product>>(json, options);

            // Thêm sản phẩm vào danh sách
            products.Add(product1);
            products.Add(product2);

            // Tạo một danh sách khách hàng
            List<Customer> customers = new List<Customer>();

            // Tạo một khách hàng mới
            Customer customer1 = new Customer("KH001", "123456", "Khách hàng 1", "0123456789", "Địa chỉ 1", "Thành viên");
            Customer customer2 = new Customer("KH002", "123456", "Khách hàng 2", "0123456789", "Địa chỉ 2", "Thành viên");

            // Thêm khách hàng vào danh sách
            customers.Add(customer1);
            customers.Add(customer2);

            // Tạo một danh sách hóa đơn
            List<Transfer> transfers = new List<Transfer>();

            // Tạo một hóa đơn mới
            Transfer transfer1 = new Transfer(1, DateTime.Parse("2024-01-01"), 1, "Tiền mặt", 100000, "KH001", new List<Product>() { product1 });
            Transfer transfer2 = new Transfer(2, DateTime.Parse("2024-01-01"), 2, "Thẻ tín dụng", 200000, "KH002", new List<Product>() { product2 });


            // Thêm hóa đơn vào danh sách
            transfers.Add(transfer1);
            transfers.Add(transfer2);
            
            

            List<Staff> staffs = new List<Staff>();
            Staff staff1 = new Staff("001", "staff1", "Thien", "0123123123", "Quan 7", "Ca 1");
            Staff staff2 = new Staff("002", "staff2", "BVao", "099999999", "BD", "Ca 2");
            staffs.Add(staff1);
            staffs.Add(staff2);

            // Lưu dữ liệu vào file JSON
            SaveDataToJson(products, customers, transfers, staffs);

            // Đọc dữ liệu từ file JSON
            LoadDataFromJson();
        }

        static void SaveDataToJson(List<Product> products, List<Customer> customers, List<Transfer> transfers, List<Staff> staffs)
        {
            // Tạo một đối tượng JsonSerializerOptions
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            // Chuyển đổi dữ liệu thành chuỗi JSON
            string productsJson = JsonSerializer.Serialize(products, options);
            string customersJson = JsonSerializer.Serialize(customers, options);
            string transfersJson = JsonSerializer.Serialize(transfers, options);
            string staffJson = JsonSerializer.Serialize(staffs, options);

            // Lưu dữ liệu vào file JSON
            File.WriteAllText("products.json", productsJson);
            File.WriteAllText("customers.json", customersJson);
            File.WriteAllText("transfers.json", transfersJson);
            File.WriteAllText("staffs.json", staffJson);

            Console.WriteLine("Dữ liệu đã được lưu vào file JSON.");
        }

        static void LoadDataFromJson()
        {
            // Kiểm tra xem file JSON có tồn tại hay không
            if (File.Exists("products.json") && File.Exists("customers.json") && File.Exists("transfers.json") && File.Exists("staffs.json"))
            {
                // Đọc dữ liệu từ file JSON
                string productsJson = File.ReadAllText("products.json");
                string customersJson = File.ReadAllText("customers.json");
                string transfersJson = File.ReadAllText("transfers.json");
                string staffsJson = File.ReadAllText("staffs.json");

                // Chuyển đổi chuỗi JSON thành đối tượng
                List<Product> products = JsonSerializer.Deserialize<List<Product>>(productsJson);
                List<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(customersJson);
                List<Transfer> transfers = JsonSerializer.Deserialize<List<Transfer>>(transfersJson);
                List<Staff> staffs = JsonSerializer.Deserialize<List<Staff>>(staffsJson);

                // Hiển thị dữ liệu
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
            else
            {
                Console.WriteLine("File JSON không tồn tại.");
            }
        }
    }
}
