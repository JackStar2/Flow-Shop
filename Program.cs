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
            Console.WriteLine("Chọn thao tác:");
            Console.WriteLine("1. Nguoi dung.");
            Console.WriteLine("2. Manager.");
            Console.WriteLine("3. Staff");
            int answer = int.Parse(Console.ReadLine());
            switch (answer)
            {
                case 1: 
                    menu.init_customer();
                    break;
            }


            Console.WriteLine(new string('-', 60));
            Console.ReadKey();
        }
    }
}
