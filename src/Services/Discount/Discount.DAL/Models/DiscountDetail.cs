namespace Discount.DAL.Models
{
    public class DiscountDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}
