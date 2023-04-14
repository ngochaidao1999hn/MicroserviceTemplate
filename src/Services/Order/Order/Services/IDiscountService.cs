using DiscountGrpc;

namespace Order.Services
{
    public interface IDiscountService
    {
        Task<DiscountResponse> GetDiscount(GetDiscountRequest request);
    }
}
