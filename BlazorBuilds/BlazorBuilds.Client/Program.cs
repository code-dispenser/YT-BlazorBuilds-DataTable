using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorBuilds.Client.Services;

namespace BlazorBuilds.Client;

class Program
{
    static async Task Main(string[] args)
    {

        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        var path    = builder.HostEnvironment.BaseAddress;

        builder.Services.AddScoped<GrpcChannel>(services =>
        {
            var channel = GrpcChannel.ForAddress(path, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()),
                MaxReceiveMessageSize =  10 * 1024 * 1024

            });
            return channel;
        });

        builder.Services.AddTransient<CustomerService>();

        await builder.Build().RunAsync();
    }
}
