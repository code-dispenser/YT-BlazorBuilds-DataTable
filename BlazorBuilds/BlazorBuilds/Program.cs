using BlazorBuilds.Components;
using BlazorBuilds.EFCore;
using BlazorBuilds.GrpcServices;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;
using System.IO.Compression;

namespace BlazorBuilds;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();


        builder.Services.AddDbContext<CustomersDbReadOnly>(options =>
        {
            options.UseSqlite("DataSource=Customers.sqlite;Mode=ReadOnly;");
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        builder.Services.AddCodeFirstGrpc(config => {
            config.ResponseCompressionLevel = CompressionLevel.Optimal;
            config.MaxSendMessageSize  = 10 * 1024 * 1024;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });

        app.MapGrpcService<GrpcCustomerService>();

        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

       

        app.Run();
    }
}
