using BlazorBuilds.Contracts.Areas.Customers;
using System.Linq.Expressions;

namespace BlazorBuilds.EFCore.Models;

public class Customer
{
    public Guid      CustomerID       { get; set; } = default!;
    public string    FirstName        { get; set; } = default!;
    public string    LastName         { get; set; } = default!;
    public string    Company          { get; set; } = default!;
    public string    City             { get; set; } = default!;
    public string    Country          { get; set; } = default!;
    public string?   PhoneNo          { get; set; } = default!;
    public string    Email            { get; set; } = default!;
    public DateTime? SubscriptionDate { get; set; } = default;
    public string    Website          { get; set; } = default!;



    public static Expression<Func<Customer, CustomerSearchResult>> ProjectToCustomerSearchResult

        => (c) => new CustomerSearchResult(c.CustomerID, c.FirstName, c.LastName, c.Company, c.Country, c.SubscriptionDate);

}