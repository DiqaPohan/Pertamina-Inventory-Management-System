using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pertamina.SolutionTemplate.Shared.Data.Commands.DetailData;

namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog.Components;

public partial class DialogDetail
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public DetailDataRequest Request { get; set; }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

}
