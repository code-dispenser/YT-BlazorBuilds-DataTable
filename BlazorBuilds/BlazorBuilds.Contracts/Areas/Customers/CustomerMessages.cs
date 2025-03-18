using ProtoBuf;

namespace BlazorBuilds.Contracts.Areas.Customers;

[ProtoContract]
public class CustomerSearch
{
    [ProtoMember(1)] public string? FirstName   { get; set; } = default!;
    [ProtoMember(2)] public string? LastName    { get; set; } = default!;
    [ProtoMember(3)] public string? CompanyName { get; set; } = default!;

    private CustomerSearch() { }

    public CustomerSearch(string? firstName, string? lastName, string? companyName)
    {
        FirstName   = String.IsNullOrWhiteSpace(firstName)   ? null : firstName!.Trim();
        LastName    = String.IsNullOrWhiteSpace(lastName)    ? null : lastName.Trim(); 
        CompanyName = String.IsNullOrWhiteSpace(companyName) ? null : companyName.Trim();

    }


}

[ProtoContract]
public class CustomerSearchResponse
{
    [ProtoMember(1)] public IEnumerable<CustomerSearchResult> SearchResults { get; set; } = new List<CustomerSearchResult>();

    public CustomerSearchResponse(IEnumerable<CustomerSearchResult> searchResults) => SearchResults = searchResults;

    private CustomerSearchResponse() { }

}



