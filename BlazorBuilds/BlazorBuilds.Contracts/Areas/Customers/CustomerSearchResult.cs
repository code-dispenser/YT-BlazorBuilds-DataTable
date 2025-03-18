using ProtoBuf;

namespace BlazorBuilds.Contracts.Areas.Customers;

[ProtoContract]
public class CustomerSearchResult
{


    [ProtoMember(1)] public Guid     CustomerID       { get;}
    [ProtoMember(2)] public string   FirstName        { get;}
    [ProtoMember(3)] public string   LastName         { get;}
    [ProtoMember(4)] public string   Company          { get;}
    [ProtoMember(5)] public string   Country          { get;}
    [ProtoMember(6)] public DateTime? SubscriptionDate { get;}
 

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CustomerSearchResult() { }

    public CustomerSearchResult(Guid customerID, string firstName, string lastName, string company, string country, DateTime? subscriptionDate) 
    {
        CustomerID       = customerID;
        FirstName        = firstName;
        LastName         = lastName;
        Company          = company;
        Country          = country;
        SubscriptionDate = subscriptionDate;

    }
}
