//using MudBlazor;
//using Pertamina.SolutionTemplate.Bsui.Common.Constants;
//using Pertamina.SolutionTemplate.Shared.Common.Constants;
//using Pertamina.SolutionTemplate.Shared.Common.Extensions;

//namespace Pertamina.SolutionTemplate.Bsui.Common.Pages;

//public partial class TabularView
//{
//    private List<BreadcrumbItem> _breadcrumbItems = new();
//    private string _greetings = default!;

//    protected override void OnInitialized()
//    {
//        SetupBreadcrumb();

//        _greetings = $"Good {DateTimeOffset.Now.ToFriendlyTimeDisplayText()}";
//    }

//    private void SetupBreadcrumb()
//    {
//        _breadcrumbItems = new()
//        {
//            CommonBreadcrumbFor.Active(CommonDisplayTextFor.Home)
//        };
//    }
//}
