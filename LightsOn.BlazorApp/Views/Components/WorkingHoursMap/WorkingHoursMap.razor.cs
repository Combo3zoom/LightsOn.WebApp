using Microsoft.AspNetCore.Components;

namespace LightsOn.BlazorApp.Views.Components.WorkingHoursMap;

public partial class WorkingHoursMap  : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; }
}