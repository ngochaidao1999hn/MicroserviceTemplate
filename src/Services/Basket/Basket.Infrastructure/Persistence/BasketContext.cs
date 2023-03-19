using Basket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Persistence
{
    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> options) : base(options)
        {
        }
        public DbSet<ShoppingCart>ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
