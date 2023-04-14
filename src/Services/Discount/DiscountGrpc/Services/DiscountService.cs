using Discount.DAL.Models;
using DiscountGrpc;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Services
{
    public class DiscountService : Discount.DiscountBase
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly DiscountDbContext _context;
        public DiscountService(ILogger<DiscountService> logger, DiscountDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        [Authorize]
        public override async Task<DiscountResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var discount = await _context.Discounts.Include(x => x.Details).Where(x => x.EventId == request.EventId && x.Details.Where(y => y.Id == request.ProductId).Any()).FirstOrDefaultAsync();
            if (discount == null)
            { 
                return new DiscountResponse {
                    DiscountId = 0,
                    Rate = 0,
                };
            }
            return new DiscountResponse
            {
                DiscountId = discount.Id,
                Rate = discount.Rate,
            };
        }
    }
}