using BlazorBuilds.Contracts.Areas.Customers;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System.Threading.Channels;

namespace BlazorBuilds.Client.Services
{
    public class CustomerService
    {
        private readonly IGrpcCustomerService _customerService;
        public CustomerService(GrpcChannel channel)

            => _customerService = channel.CreateGrpcService<IGrpcCustomerService>();

        public async Task<CustomerSearchResponse> CustomerSearch(CustomerSearch query)
            
            => await _customerService.CustomerSearch(query);
    }
}
