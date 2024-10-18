using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

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
    public static void init_manager()
    {
        string fileManager = "managerList.json";

        // Kiểm tra xem file JSON có tồn tại không
        list_Manager managerList;
        if (File.Exists(fileManager))
        {
            // Đọc dữ liệu từ file JSON nếu file tồn tại
            string jsonManager = File.ReadAllText(fileManager);
            managerList = JsonSerializer.Deserialize<list_Manager>(jsonManager) ?? new list_Manager();
        }
        else
        {
        
            managerList = new list_Manager();
        }

        Console.WriteLine("Nhập thông tin quản lí:");
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

        managerList.Add(new Manager(id, password, name, phone, address));

        // Ghi lại toàn bộ danh sách khách hàng vào file JSON
        string newJsonManager = JsonSerializer.Serialize(managerList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileManager, newJsonManager);

        Console.WriteLine("Danh sách quản lí:");
        Console.WriteLine(new string('-', 60));  // Dòng phân cách
        Console.WriteLine($"{"ID",-10}{"Tên",-15}{"SĐT",-15}{"Địa chỉ",-10}{"Quản lí",-10}");
        Console.WriteLine(new string('-', 60));  // Dòng phân cách

        if (managerList.Managers.Count > 0)
        {
            for (int i = 0; i < managerList.Managers.Count; i++)
            {
                Manager manager = managerList.Managers[i];
                Console.WriteLine($"{manager.User_id,-10}{manager.User_name,-15}{manager.User_Phone,-15}{manager.User_Address,-10};
            }
        }
        else
        {
            Console.WriteLine("Danh sách quản lí trống.");
        }
    }
    public static void init_staff()
    {
        string fileStaff = "staffList.json";

        // Kiểm tra xem file JSON có tồn tại không
        list_Staff staffList;
        if (File.Exists(fileStaff))
        {
            // Đọc dữ liệu từ file JSON nếu file tồn tại
            string jsonStaff = File.ReadAllText(fileStaff);
            staffList = JsonSerializer.Deserialize<list_Staff>(jsonStaff) ?? new list_Staff();
        }
        else
        {
           
            staffList = new list_Staff();
        }

        Console.WriteLine("Nhập thông tin nhân viên:");
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
        Console.Write("Ca: ");
        string StaffShift = Console.ReadLine();

        staffList.Add(new Staff(id, password, name, phone, address, StaffShift));

        string newJsonStaff = JsonSerializer.Serialize(staffList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileStaff, newJsonStaff);

        Console.WriteLine("Danh sách quản lí:");
        Console.WriteLine(new string('-', 60));  // Dòng phân cách
        Console.WriteLine($"{"ID",-10}{"Tên",-15}{"SĐT",-15}{"Địa chỉ",-10}{"Ca",-10}");
        Console.WriteLine(new string('-', 60));  // Dòng phân cách

        if (staffList.Staffs.Count > 0)
        {
            for (int i = 0; i < staffList.Staffs.Count; i++)
            {
                Staff staff = staffList.Staffs[i];
                Console.WriteLine($"{staff.User_id,-10}{staff.User_name,-15}{staff.User_Phone,-15}{staff.User_Address,-10}{staff.StaffShift,-10}");
            }
        }
        else
        {
            Console.WriteLine("Danh sách nhân viên trống.");
        }
    }
}
