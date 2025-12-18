using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Syncfusion.Blazor.Grids;
using Pertamina.SolutionTemplate.Bsui.Features.Catalog.Constants;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleRequestData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleDataDraftHistoricalApplicationPhase;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog;

public partial class Request
{
    private bool _isLoading;
    private ErrorResponse? _error;
    private readonly List<BreadcrumbItem> _breadcrumbItems = new();
    private int _totalappsnewpending = default!;
    private int _totalappseditpending = default!;

    private SfGrid<GetSingleRequestData>? GridRequested { get; set; }
    //private SfGrid<GetSingleDataDraftHistoricalApplicationPhase>? GridHistorical { get; set; }
    private void SetupBreadcrumb()
    {
        _breadcrumbItems.Add(CommonBreadcrumbFor.Catalog);
        _breadcrumbItems.Add(CommonBreadcrumbFor.Active(CommonDisplayTextFor.RequestAndApproval));
    }
    private List<GetSingleRequestData> _dataRequestCount = default!;
    //private List<GetSingleDataDraftHistoricalApplicationPhase> _dataHistoricalCount = default!;
    public List<GetSingleRequestData> GridDataRequest { get; set; }
    //public List<GetSingleDataDraftHistoricalApplicationPhase> GridDataHistorical { get; set; }
    protected override async Task OnInitializedAsync()
    {
        SetupBreadcrumb();
        _isLoading = true;
        _totalappsnewpending = 9;
        _totalappseditpending = 2;
        _isLoading = false;
        await GetDatas();
    }
    private async Task GetDatas()
    {
        _dataRequestCount = new List<GetSingleRequestData>();
        //_dataHistoricalCount = new List<GetSingleDataDraftHistoricalApplicationPhase>();
        _isLoading = true;
        var response = await _dataService.GetAllRequestDataAsync();
        //var responseDraftHistorical = await _dataService.GetAllDraftHistoricalApplicationPhaseDataAsync();
        if (response.Error is not null)
        {
            _isLoading = false;
            _error = response.Error;
            return;
        }
        else
        {
            _dataRequestCount.AddRange(response.Result!.Items);
            //_dataHistoricalCount.AddRange(responseDraftHistorical.Result!.Items);
            _isLoading = false;
        }
    }
    private void CommandClickGridHistorical(CommandClickEventArgs<GetSingleDataDraftHistoricalApplicationPhase> args)
    {
        var rowIndex = args.RowData;
        var url = $"{RouteFor.ViewDraftHistoricalApplicationPhase(rowIndex.Code_Apps)}";
        var newurl = _navigationManager.ToAbsoluteUri(url).AbsolutePath;
        _navigationManager.NavigateTo(newurl, true);
    }
    private void CommandClickGridRequested(CommandClickEventArgs<GetSingleRequestData> args)
    {
        var rowIndex = args.RowData;
        var url = $"{RouteFor.ViewDraftCatalog(rowIndex.Code_Apps)}";
        var newurl = _navigationManager.ToAbsoluteUri(url).AbsolutePath;
        _navigationManager.NavigateTo(newurl, true);
    }
    public void RowBoundGridRequest(RowDataBoundEventArgs<GetSingleRequestData> args)
    {
        if (!string.IsNullOrEmpty(args.Data.IsApproved))
        {
            if (args.Data.IsApproved == "Approved")
            {
                args.Row.AddClass(new string[] { "status-aktif" });
            }
            else if (args.Data.IsApproved == "Rejected")
            {
                args.Row.AddClass(new string[] { "status-nonaktif" });
            }
            else
            {
                args.Row.AddClass(new string[] { "status-tidakdiketahui" });
            }
        }
        else
        {
            args.Row.AddClass(new string[] { "status-nonaktif" });
        }
    }
    public void RowBoundGridHistorical(RowDataBoundEventArgs<GetSingleDataDraftHistoricalApplicationPhase> args)
    {
        if (!string.IsNullOrEmpty(args.Data.IsApproved))
        {
            if (args.Data.IsApproved == "Approved")
            {
                args.Row.AddClass(new string[] { "status-aktif" });
            }
            else if (args.Data.IsApproved == "Rejected")
            {
                args.Row.AddClass(new string[] { "status-nonaktif" });
            }
            else
            {
                args.Row.AddClass(new string[] { "status-tidakdiketahui" });
            }
        }
        else
        {
            args.Row.AddClass(new string[] { "status-nonaktif" });
        }
    }
}
