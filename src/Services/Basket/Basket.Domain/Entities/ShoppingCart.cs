using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Domain.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {
        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }
    }
}
