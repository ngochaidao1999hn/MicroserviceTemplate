using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Domain.Entities
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
