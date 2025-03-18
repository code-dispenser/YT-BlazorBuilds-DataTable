using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBuilds.Contracts.Areas.Customers;

[Service]
public interface IGrpcCustomerService
{


    [Operation]
    Task<CustomerSearchResponse> CustomerSearch(CustomerSearch instruction, CallContext context = default);

 
}