using Microsoft.EntityFrameworkCore;

namespace Discount.DAL.Models
{
    public class DiscountDbContext : DbContext
    {
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {
        }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountDetail> DiscountDetails { get; set; }
    }
}
