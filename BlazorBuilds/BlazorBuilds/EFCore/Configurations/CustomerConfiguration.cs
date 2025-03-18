using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BlazorBuilds.EFCore.Models;
namespace BlazorBuilds.EFCore.Configurations;

public partial class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.Property(e => e.CustomerID)
            .ValueGeneratedNever()
            .HasColumnType("varchar(50)");
        entity.Property(e => e.City)
            .HasColumnType("varchar(50)");
        entity.Property(e => e.Company)
            .HasColumnType("varchar(50)");
        entity.Property(e => e.Country)
            .HasColumnType("varchar(75)");
        entity.Property(e => e.Email)
            .HasColumnType("varchar(50)");
        entity.Property(e => e.FirstName)
            .HasColumnType("varchar(50)");
        entity.Property(e => e.LastName)
            .HasColumnType("varchar(50)");
        entity.Property(e => e.PhoneNo)
            .HasColumnType("varchar(50)");
        entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");
        entity.Property(e => e.Website)
            .HasColumnType("varchar(50)");
    }
}
