using Discount.Api.Models;

namespace Discount.DAL.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public double Rate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ExpireDate { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public List<DiscountDetail> Details { get; set; }
    }
}
