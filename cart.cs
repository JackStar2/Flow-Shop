using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_cuoi_ki
{
    public class Cart
    {
        public List<Product> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public decimal Discount { get; set; }


        public Cart(string userId)
        {
            Items = new List<Product>();
            TotalPrice = 0;
            UserId = userId;
            TimeCreated = DateTime.Now;
            TimeUpdated = DateTime.Now;
            Discount = 0;
        }


        public void AddItem(Product product, int quantity)
        {
            if (product.Expired())
            {
                Console.WriteLine($"Product {product.ProductName} đã hết hạn và không thể thêm.");
                return;
            }
            if (!product.CheckStock(quantity))
            {
                Console.WriteLine($"Không đủ hàng cho sản phẩm {product.ProductName}.");
                return;
            }

            product.ReduceStock(quantity);
            product.ProductQuantity = quantity;
            Items.Add(product);
            UpdateTotalPrice();
            TimeUpdated = DateTime.Now;
        }

        public void RemoveItem(Product product)
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


        public List<Product> GetItems()
        {
            return Items;
        }

        private void UpdateTotalPrice()
        {
            TotalPrice = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                TotalPrice += Items[i].ProductPrice * Items[i].ProductQuantity;
            }
            TotalPrice -= Discount;
        }
    }
}
   