using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Extensions;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
namespace Pertamina.SolutionTemplate.Bsui.Features.Excel.Upload.MasterData.Raw.LembarKerja;

public partial class Index
{
    private bool _isLoading;
    private ErrorResponse? _error;
    private readonly List<BreadcrumbItem> _breadcrumbItems = new();
    private string _greetings = default!;
    private void SetupBreadcrumb()
    {
        _breadcrumbItems.Add(CommonBreadcrumbFor.Catalog);
        _breadcrumbItems.Add(CommonBreadcrumbFor.Active(CommonDisplayTextFor.Overview));
    }
    private void OnInitialized()
    {
        SetupBreadcrumb();
        _greetings = $"Good {DateTimeOffset.Now.ToFriendlyTimeDisplayText()}";
    }
}
