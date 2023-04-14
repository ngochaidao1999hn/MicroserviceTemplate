using DiscountGrpc;
using Grpc.Core;
using Grpc.Net.Client;

namespace Order.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly GrpcChannel _channel;
        private readonly Discount.DiscountClient _client;
        private readonly IConfiguration _configuration;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _channel =
               GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:DiscountServiceUrl"));
            _client = new Discount.DiscountClient(_channel);
        }

        public async Task<DiscountResponse> GetDiscount(GetDiscountRequest request)
        {
            var metadata = new Metadata
            {
                { "Authorization", "Bearer your_jwt_token" }
            };
            var options = new CallOptions(metadata);
            return await _client.GetDiscountAsync(request, options);            
        }
    }
}
