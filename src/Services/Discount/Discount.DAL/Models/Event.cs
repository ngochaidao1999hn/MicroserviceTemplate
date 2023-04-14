using Discount.DAL.Commons;
using Discount.Api.Models;

namespace Discount.Api.Models
{
    public class Event : IBaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeleteAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}
        public ICollection<Discount.DAL.Models.Discount> Discounts { get; set; }
    }
}
