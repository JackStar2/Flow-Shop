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
        List<Product> ListItem = new List<Product>();

        public int Transfer_ID { get => TransferID; set => TransferID = value; }
        public DateTime Date_ { get => Date; set => Date = value; }
        public int Cashier_ID { get => CashierID; set => CashierID = value; }
        public string Payment_Method { get => PaymentMethod; set => PaymentMethod = value; }
        public decimal Customer_Money { get => CustomerMoney; set => CustomerMoney = value; }
        public int Customer_ID { get => CustomerID; set => CustomerID = value; }
        public List<Product> List_Item { get => ListItem; set => ListItem = value; }

        public Transfer(int transferID, DateTime date, int cashierID, string paymentMethod, decimal customerMoney, int customerID, List<Product> listItem)
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
                total += List_Item[i].ProductPrice * List_Item[i].ProductQuantity;
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

        public void Add_Item(Product product)
        {
            List_Item.Add(product);
        }

        public void Remove_Item(Product product)
        {
            List_Item.Remove(product);
        }
    }
}


   