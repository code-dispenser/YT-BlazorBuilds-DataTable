using BlazorBuilds.Contracts.Areas.Customers;
using BlazorBuilds.EFCore;
using BlazorBuilds.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc;

namespace BlazorBuilds.GrpcServices
{
    public class GrpcCustomerService : IGrpcCustomerService
    {
        private readonly CustomersDbReadOnly _readOnlyDB;
        public GrpcCustomerService(CustomersDbReadOnly readOnlyDB)

            => _readOnlyDB = readOnlyDB;


        public async Task<CustomerSearchResponse> CustomerSearch(CustomerSearch instruction, CallContext context)
        {

            var searchResults = await _readOnlyDB.Customers.Where(c => (instruction.LastName == null || c.LastName.StartsWith(instruction.LastName))
                                                                && (instruction.FirstName == null || c.FirstName.StartsWith(instruction.FirstName))
                                                                && (instruction.CompanyName == null || c.Company.StartsWith(instruction.CompanyName)))

                                            .Select(Customer.ProjectToCustomerSearchResult)
                                            .ToListAsync();

            return new CustomerSearchResponse(searchResults);
            
        }

        
    }
}
