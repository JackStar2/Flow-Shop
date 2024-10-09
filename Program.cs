using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_cuoi_ki
{
    public class Transfer
    {
        private int TransferID;
        private DateTime Date;
        private int CashierID;
        private string PaymentMethod;
        private decimal CustomerMoney;
        private int CustomerID;
        List<Products> ListItem = new List<Products>();

        public int Transfer_ID { get => TransferID; set => TransferID = value; }
        public DateTime Date_ { get => Date; set => Date = value; }
        public int Cashier_ID { get => CashierID; set => CashierID = value; }
        public string Payment_Method { get => PaymentMethod; set => PaymentMethod = value; }
        public decimal Customer_Money { get => CustomerMoney; set => CustomerMoney = value; }
        public int Customer_ID { get => CustomerID; set => CustomerID = value; }
        public List<Products> List_Item { get => ListItem; set => ListItem = value; }

        public Transfer(int transferID, DateTime date, int cashierID, string paymentMethod, decimal customerMoney, int customerID, List<Products> listItem)
        {
            TransferID = transferID;
            Date = date;
            CashierID = cashierID;
            PaymentMethod = paymentMethod;
            CustomerMoney = customerMoney;
            CustomerID = customerID;
            ListItem = listItem;
        }
 
        public decimal Total_Price()
        {
            decimal total = 0;
            for (int i = 0; i < List_Item.Count; i++)
            {
                total += List_Item[i].Price * List_Item[i].Product_Quantity; 
            }
            return total;
        }

        public decimal Exchange()
        {
            decimal total = Total_Price();
            decimal exchange = Customer_Money - total;

            if (exchange < 0)
            {
                Console.WriteLine("Số tiền không đủ để thanh toán.");
                return 0; 
            }

            return exchange;
        }

        public void Add_Item(Products product)
        {
            List_Item.Add(product);
        }

        public void Remove_Item(Products product)
        {
            List_Item.Remove(product);
        }
    }
    public class Cart
    {
        public List<Products> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public decimal Discount { get; set; }

     
        public Cart(string userId)
        {
            Items = new List<Products>();
            TotalPrice = 0;
            UserId = userId;
            TimeCreated = DateTime.Now;
            TimeUpdated = DateTime.Now;
            Discount = 0;
        }

    
        public void AddItem(Products product, int quantity)
        {
            if (product.Expired())
            {
                Console.WriteLine($"Product {product.Product_Name} đã hết hạn và không thể thêm.");
                return;
            }
            if (!product.CheckStock(quantity))
            {
                Console.WriteLine($"Không đủ hàng cho sản phẩm {product.Product_Name}.");
                return;
            }

            product.ReduceStock(quantity);
            product.Product_Quantity = quantity; 
            Items.Add(product);
            UpdateTotalPrice();
            TimeUpdated = DateTime.Now;
        }

        public void RemoveItem(Products product)
        {
            Items.Remove(product);
            UpdateTotalPrice();
            TimeUpdated = DateTime.Now;
        }

        public decimal GetTotalPrice()
        {
            return TotalPrice;
        }

        public void ClearCart()
        {
            Items.Clear();
            TotalPrice = 0;
            TimeUpdated = DateTime.Now;
        }

        public void Checkout(Transfer transfer)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                transfer.Add_Item(Items[i]); 
            }

            decimal total = transfer.Total_Price();
            decimal change = transfer.Exchange();

            if (change >= 0) 
            {
                Console.WriteLine($"Tổng số tiền: {total}");
                Console.WriteLine($"Số tiền thừa: {change}");
                ClearCart();
            }
        }
   

        public List<Products> GetItems()
        {
            return Items;
        }

        private void UpdateTotalPrice()
        {
            TotalPrice = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                TotalPrice += Items[i].Product_Price * Items[i].Product_Quantity;
            }
            TotalPrice -= Discount;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
