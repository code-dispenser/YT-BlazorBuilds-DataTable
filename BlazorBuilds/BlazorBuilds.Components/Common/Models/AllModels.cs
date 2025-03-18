namespace BlazorBuilds.Components.Common.Models;

public class PagingInfo(int currentPage, int itemCount, int totalItemCount, int itemsPerPage)
{
    public int CurrentPage    { get; set; } = currentPage;
    public int ItemCount      { get; set; } = itemCount;
    public int TotalItemCount { get; set; } = totalItemCount;
    public int ItemsPerPage   { get; set; } = itemsPerPage;
}

public record DebouncedTextResult(string TextValue, bool IsValid, string? ExceptionMessage = null);