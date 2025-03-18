using BlazorBuilds.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorBuilds.EFCore;

public class CustomersDbReadOnly : DbContext
{
    public virtual DbSet<Customer> Customers { get; set; }

    public CustomersDbReadOnly(DbContextOptions<CustomersDbReadOnly> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)

        => modelBuilder.ApplyConfiguration(new Configurations.CustomerConfiguration());

    public override int SaveChanges()

        => -1;

}