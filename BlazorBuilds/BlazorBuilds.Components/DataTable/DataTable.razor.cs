using BlazorBuilds.Components.Common.Enums;
using BlazorBuilds.Components.Common.Extensions;
using BlazorBuilds.Components.Common.Models;
using BlazorBuilds.Components.Common.Seeds;
using BlazorBuilds.Components.Common.Utilities;
using Microsoft.AspNetCore.Components;
using System.Data;
using System.Diagnostics;


namespace BlazorBuilds.Components.DataTable;

public partial class DataTable<TData> 
{
    [Parameter, EditorRequired] public string  AriaTableTitle { get; set; } = default!;
    [Parameter, EditorRequired] public List<TData> DataSource { get; set; } = [];
    [Parameter] public Func<TData, bool>?          FilterRule { get; set; }

    [Parameter] public RenderFragment  DataTableColumns       { get; set; } = default!;
    [Parameter] public RenderFragment? TopPager               { get; set; }
    [Parameter] public RenderFragment? BottomPager            { get; set; }
    [Parameter] public RenderFragment? Filter                 { get; set; }
    [Parameter] public int             DefaultSortColumnIndex { get; set; } = -1;
    [Parameter] public PagingInfo?     PagingInfo             { get; set; }
    [Parameter] public string?         AriaSelectRowLabel     { get; set; }
    [Parameter] public string?         NoDataLabel            { get; set; }
    [Parameter] public bool            ShowTableTitle         { get; set; } = true;
    [Parameter] public int             VirtualizeItemSizePX   { get; set; } = 32;
    [Parameter] public SelectionMode   SelectionMode          { get; set; } = SelectionMode.None;
    [Parameter] public List<TData>     SelectedRows           { get; set; } = [];
    [Parameter] public Func<TData,string?>? RowStyleFunc      { get; set; } = null;

    [Parameter] public EventCallback<List<TData>> SelectedRowsChanged { get; set; }


    private readonly List<ColumnBase<TData>>    _dataTableColumns   = [];
    private readonly Dictionary<string, string> _columnAlignments   = [];

    private List<TData>        _dataPage           = [];
    private Func<TData, bool>? _previousFilterRule = null;
    
    private readonly string _tableTitleID = Guid.NewGuid().ToString();
    private string          _noDataLabel  = GlobalValues.DataTable_No_Records_Message;

    private bool        _showTableSpinner = false;
    private bool        _usesPaging       = false;
    private List<TData> _previousDataRef  = [];
    private List<TData> _dataSource       = [];
    private List<TData> _selectedRows     = [];
    private string      _selectRowLabel   = GlobalValues.DataTable_Aria_Select_Row_Label;
    private int         _lastSortedColumnIndex = -1;


    protected override async Task OnParametersSetAsync()
    {
        var rowsChanged = false;
     
        if (false == ReferenceEquals(_previousDataRef,DataSource)) //new search / datasource
        {
  
            _selectedRows.Clear();//new datasource so different equality, we could end up with duplicates if left.
            _previousDataRef = DataSource;
            _dataSource      = DataSource;

            await CheckAndResortLastColumn(_dataSource,_lastSortedColumnIndex);
            _usesPaging.WhenTrue(() => CheckSetPagingInfo(DataSource, true));
        }
        else if( (_usesPaging == true && DataSource.Count != PagingInfo!.TotalItemCount))//existing data source with items added or deleted
        {
            rowsChanged = true;
            if(DataSource.Count < (PagingInfo.CurrentPage * PagingInfo.ItemsPerPage))//move to new last page if page is no good
            {
                PagingInfo.CurrentPage = DataSource.Count < 1 ? 0 : (int)Math.Ceiling((double)DataSource.Count / PagingInfo.ItemsPerPage);
            }
            PagingInfo!.TotalItemCount = DataSource.Count;
            PagingInfo.ItemCount       = DataSource.Count;
        }

        if (FilterRule is not null && (FilterRule != _previousFilterRule || true == rowsChanged))
        {
            await ToggleTableSpinner(true, DataSource.Count);
            _previousFilterRule = FilterRule;
            _dataSource         = [.. DataSource.Where(FilterRule)];
            await CheckAndResortLastColumn(_dataSource,_lastSortedColumnIndex);
            await ToggleTableSpinner(false);
            _usesPaging.WhenTrue(() => CheckSetPagingInfo(_dataSource,false));
        }

        _selectedRows  = SelectedRows ?? [];
        _usesPaging.WhenTrueElse(() => _dataPage = GetDataPage(PagingInfo!.CurrentPage, PagingInfo.ItemsPerPage, _dataSource),() => _dataPage = _dataSource);
    }

    protected override void OnInitialized()
    {
        _selectRowLabel   = String.IsNullOrWhiteSpace(AriaSelectRowLabel) ? GlobalValues.DataTable_Aria_Select_Row_Label : AriaSelectRowLabel.Trim();
        _noDataLabel      = String.IsNullOrWhiteSpace(NoDataLabel)        ? GlobalValues.DataTable_No_Records_Message    : NoDataLabel.Trim();
        _usesPaging       = PagingInfo != null;
        _previousDataRef  = DataSource;
        _dataSource       = DataSource;
    }
    private async Task CheckAndResortLastColumn(List<TData> dataSource, int lastSortedColumnIndex)
    {
        if (lastSortedColumnIndex != -1)
        {
            var dataColumn = _dataTableColumns[lastSortedColumnIndex];
            await SortDataSource((dataColumn.SortDirection == SortDirection.Ascending), dataSource, dataColumn);
        }
    }
    private void CheckSetPagingInfo(List<TData> dataSource, bool isNewData)
    {
        if (PagingInfo is not null)
        {
            PagingInfo.CurrentPage = dataSource.Count > 0 ? 1 : 0;
            PagingInfo.ItemCount   = dataSource.Count;

           isNewData.WhenTrue(() => PagingInfo.TotalItemCount = dataSource.Count);
        }
    }

    public void AddDataTableColumn(ColumnBase<TData> dataColumn)
    {
        _columnAlignments[dataColumn.FieldName] = dataColumn.Alignment switch { Alignment.Right => GlobalValues.Text_Align_Right, Alignment.Center => GlobalValues.Text_Align_Centre, _ => GlobalValues.Text_Align_Left };
        _dataTableColumns.Contains(dataColumn).WhenFalse(() => _dataTableColumns.Add(dataColumn));
    }
    
    public void RemoveDataTableColumn(ColumnBase<TData> dataColumn)

        => _dataTableColumns.Contains(dataColumn).WhenTrue(() => _dataTableColumns.Remove(dataColumn));

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (true == firstRender)
        {                                                                //Not sorting on template columns, only data property columns
            if (DefaultSortColumnIndex > -1 && DefaultSortColumnIndex < _dataTableColumns.Where(a => a is DataPropertyColumn<TData>).ToList().Count)
            {
                _lastSortedColumnIndex = await ToggleSortData(_dataTableColumns[DefaultSortColumnIndex], _dataSource);

                _usesPaging.WhenTrue(() => _dataPage = GetDataPage(PagingInfo!.CurrentPage, PagingInfo.ItemsPerPage, _dataSource));

                await InvokeAsync(StateHasChanged);
            }
        }
    }
    private async Task HandleColumnHeaderClick(DataPropertyColumn<TData> column)
    {
        if (true == column.IsSortable)
        {
            _lastSortedColumnIndex = await ToggleSortData(column, _dataSource);

            if (true ==_usesPaging)
            {
                PagingInfo!.CurrentPage = 1;//comment out if you do not like sorting to restart the paging
                _dataPage = GetDataPage(PagingInfo.CurrentPage, PagingInfo.ItemsPerPage, _dataSource);
            }
        }
    }
    private async Task<int> ToggleSortData(ColumnBase<TData> dataColumn, List<TData> dataSource)
    {
        await ToggleTableSpinner(true, dataSource.Count);

        var sortDirection = dataColumn.SortDirection;

        foreach (var column in _dataTableColumns) column.SortDirection = SortDirection.NotSorted;

        dataColumn.SortDirection = sortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
        
        await SortDataSource((dataColumn.SortDirection == SortDirection.Ascending), dataSource, dataColumn);

        await ToggleTableSpinner(false);

        return _dataTableColumns.IndexOf(dataColumn);
    }

    private static async Task SortDataSource(bool sortAscending, List<TData> dataSource, ColumnBase<TData> dataColumn)
    {

        if (dataColumn.PropertyInfo == null) return;

        var propertyValueGetter = ExpressionUtilities.CreatePropertyValueGetter<TData>(dataColumn.PropertyInfo);

        List<TData> sortedData = [];

        sortAscending.WhenTrueElse(() => sortedData = [.. dataSource.OrderBy(item => propertyValueGetter(item))], () => sortedData = [.. dataSource.OrderByDescending(item => propertyValueGetter(item))]);

        //Do in place swap with sorted results to keep equality reference
        for (int index = 0; index < dataSource.Count; index++) dataSource[index] = sortedData[index];

        await Task.CompletedTask;
    }


    private static List<TData> GetDataPage(int currentPage, int itemsPerPage, List<TData> dataSource)
    {
        if (dataSource.Count == 0) return [];

        int start = (currentPage - 1)  * itemsPerPage;

        return [.. dataSource.Skip(start).Take(itemsPerPage)];
    }

    private async Task HandleOnSelectedRow(TData rowItem)
    {
        if (SelectionMode == SelectionMode.None) return;

        _selectedRows.Contains(rowItem).WhenTrueElse(() => _selectedRows.Remove(rowItem), () => _selectedRows.Add(rowItem));
        
        (SelectionMode == SelectionMode.Single).WhenTrue(() => _selectedRows.RemoveAll(row => !row!.Equals(rowItem)));
        
        await SelectedRowsChanged.HasDelegate.WhenTrue(() => SelectedRowsChanged.InvokeAsync(_selectedRows));
    }

    private async Task ToggleSelection(bool isSelected, TData rowItem)
    {
        (true == isSelected && false == _selectedRows.Contains(rowItem)).WhenTrue(() => _selectedRows.Add(rowItem));

        isSelected.WhenFalse(() => _selectedRows.Remove(rowItem));
        
        (SelectionMode == SelectionMode.Single).WhenTrue(() => _selectedRows.RemoveAll(row => !row!.Equals(rowItem)));

        await SelectedRowsChanged.HasDelegate.WhenTrueElse(() => SelectedRowsChanged.InvokeAsync(_selectedRows), () => InvokeAsync(StateHasChanged));
    }

    private async Task ToggleTableSpinner(bool showSpinner, int rowCount = 0)
    {
        _showTableSpinner = showSpinner;
        /*
            * In this instance / context given where the code is called from you should use Task.Yield but I noticed that on the very odd occasion with filtering 
            * the spinner did not show so used the hack of Task.Delay(1) which solved the issue.
        */
        if (true == showSpinner && rowCount >= 2500) await Task.Delay(1);
    }

    private static string BuildClassList(params string[] classList)

        => String.Join(" ", classList.Where(c => !string.IsNullOrWhiteSpace(c)));

    private string GetAlignmentClass(string fieldName)

        => _columnAlignments.GetValueOrDefault(fieldName, String.Empty);

    private static string GetSortIconClass(SortDirection sortDirection)

        => sortDirection switch { SortDirection.Ascending => GlobalValues.DataTable_Sort_UP_Icon_Name_Class, SortDirection.Descending => GlobalValues.DataTable_Sort_Down_Icon_Name_Class, _ => GlobalValues.DataTable_Not_Sorted_Icon_Name_Class};

}
