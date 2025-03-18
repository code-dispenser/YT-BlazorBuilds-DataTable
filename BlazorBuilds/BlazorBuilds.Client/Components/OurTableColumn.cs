using Microsoft.AspNetCore.Components;

namespace BlazorBuilds.Client.Components;

public class OurTableColumn<TData> : ComponentBase
{
    [CascadingParameter] public StepThreeTable<TData> ParentTable { get; set; } = default!;//This gets populated by the CascadingValue that we defined the parent component
    [Parameter]          public string                FieldName   { get; set; } = default!;
    [Parameter]          public string                Title       { get; set; } = default!;

    //All we need to do is when this component is initialized, is add it to the parent component, job done.
    protected override void OnInitialized()

        => ParentTable?.AddColumn(this);
 
}
