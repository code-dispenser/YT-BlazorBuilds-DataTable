using Microsoft.AspNetCore.Components;

namespace BlazorBuilds.Components.DataTable;

public class DataTemplateColumn<TData> : ColumnBase<TData>
{
    [Parameter] public RenderFragment<string>? HeaderTemplate { get; set; } = default!;
    protected override void OnInitialized()
    {
        _fieldName = Guid.NewGuid().ToString();//just something to get alignments as these get added to a dictionary with the field as the key
        _title     = String.IsNullOrWhiteSpace(DisplayName) ? String.Empty : DisplayName;
        base.OnInitialized();//Other stuff gets set in the column base and the column is added to the parent, the data table.
    }
}
