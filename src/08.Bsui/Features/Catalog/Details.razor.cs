using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Common.Constants;
using Pertamina.SolutionTemplate.Bsui.Features.Catalog.Components;
using Pertamina.SolutionTemplate.Shared.Common.Extensions;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Commands.DetailData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog;

public partial class Details
{
    [Parameter]
    public string? Id { get; set; }
    [Parameter]
    public string? Id1 { get; set; }

    private bool _isLoading;
    private ErrorResponse? _error;
    private ListResponse<GetSingleData> _dataCount = default!;
    private readonly string _title = $"Example 1";
    private string _greetings = default!;
    private readonly List<BreadcrumbItem> _breadcrumbItems = new();
    protected override async Task OnInitializedAsync()
    {
        SetupBreadcrumb();

        _greetings = $"Good {DateTimeOffset.Now.ToFriendlyTimeDisplayText()}";
        await GetDatas();
    }

    private async Task GetDatas()
    {
        _isLoading = true;

        var response = await _dataService.GetAllDataAsync();

        _isLoading = false;

        if (response.Error is not null)
        {
            _error = response.Error;

            return;
        }

        _dataCount = response.Result!;
        if (response.Result is not null)
        {
            if (response.Result.Items.Count > 0)
            {
                var tempdata = response.Result.Items.Where(pp => pp.Capability_Level_1 == Id && pp.Capability_Level_2 == Id1).ToList();
                _dataCount.Items.Clear();
                _dataCount.Items = tempdata;
            }
        }

    }
    private void SetupBreadcrumb()
    {
        _breadcrumbItems.Add(CommonBreadcrumbFor.Catalog);
        _breadcrumbItems.Add(CommonBreadcrumbFor.BuildingBlock);
        _breadcrumbItems.Add(CommonBreadcrumbFor.Active(Id + " " + Id1));

    }
    private async Task ShowDialog(string strapp, string level1)
    {
        var request = new DetailDataRequest();

        try
        {
            var foundata = _dataCount.Items.Where(pp => pp.Application_Name == strapp && pp.Capability_Level_1 == level1).ToList();
            request.Appdesc = foundata.FirstOrDefault().Description;
            request.Appowner = foundata.FirstOrDefault().Business_Owner_PIC;
            request.Appownerpic = foundata.FirstOrDefault().Business_Owner_PIC_Email;
            request.Appownerdev = foundata.FirstOrDefault().Developer;
            request.Applink = foundata.FirstOrDefault().Link_Application;
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        var parameters = new DialogParameters
        {
            { nameof(DialogDetail.Request), request }
        };

        var dialog = _dialogService.Show<DialogDetail>(strapp, parameters);
        var result = await dialog.Result;

    }
}
