using BlazorBuilds.Components.Common.Enums;
using BlazorBuilds.Components.Common.Extensions;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Reflection;

namespace BlazorBuilds.Components.DataTable;

public abstract class ColumnBase<TData> : ComponentBase, IDisposable
{
    [CascadingParameter] protected DataTable<TData> ParentTable { get; set; } = default!;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Parameter] public RenderFragment<TData>? CellTemplate { get; set; } = default!;
    [Parameter] public string?                HeaderStyle  { get; set; } = null;
    [Parameter] public string?                CellStyle    { get; set; } = null;
    [Parameter] public string                 DisplayName  { get; set; } = default!;
    [Parameter] public string?                Format       { get; set; } = default;
    [Parameter] public Alignment              Alignment    { get; set; } = Alignment.Left;

    public PropertyInfo? PropertyInfo  { get; protected set; } = null;
    public SortDirection SortDirection { get; set; }           = SortDirection.NotSorted;

    public string FieldName     => _fieldName;
    public string Title         => _title;
    public bool HasFormatString => _hasStringFormat;
    public string FormatString  => _formatString;

    protected string _fieldName       = String.Empty;
    protected string _title           = String.Empty;
    protected string _formatString    = "{0}";
    protected bool   _hasStringFormat = false;


    protected override void OnInitialized()
    {
        _formatString    = String.IsNullOrWhiteSpace(Format) ? "{0}" : $"{{0:{Format.Trim()}}}";
        _hasStringFormat = _formatString != "{0}";

        ParentTable.AddDataTableColumn(this);
    }

    public void Dispose()
    {
        (ParentTable is not null).WhenTrue(() => ParentTable!.RemoveDataTableColumn(this));
        GC.SuppressFinalize(this);
    }
}
