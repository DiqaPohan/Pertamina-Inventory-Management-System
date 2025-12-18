using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;
using Syncfusion.Blazor.Grids;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog;

public partial class Tabular
{

    private bool _isLoading;
    private ErrorResponse? _error;
    private List<GetSingleData> _dataCount = default!;
    private readonly List<BreadcrumbItem> _breadcrumbItems = new();
    private int _totalapps = default!;
    private int _totalappskpipusat = default!;
    private int _totalappskpiru = default!;
    private int _totalappsholding = default!;
    private int _totalappsbispro = default!;
    private int _totalappscapability = default!;
    private int _totalappsstatus = default!;
    private int _totalappsurl = default!;
    private int _totalgolive = default!;
    private int _totalappsareakosong = default!;
    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationStateTask { get; set; } = default!;
    private ClaimsPrincipal _user = default!;

    protected override async Task OnInitializedAsync()
    {
        _user = (await AuthenticationStateTask).User;

        SetupBreadcrumb();

        await GetDatas();
    }
    private void SetupBreadcrumb()
    {
        _breadcrumbItems.Add(CommonBreadcrumbFor.Catalog);
        _breadcrumbItems.Add(CommonBreadcrumbFor.Active(CommonDisplayTextFor.ListApps));
    }
    private SfGrid<GetSingleData>? _blazorDataGrid;
    private async Task GetDatas()
    {
        _dataCount = new List<GetSingleData>();
        _isLoading = true;
        var response = await _dataService.GetAllDataAsync();
        if (response.Error is not null)
        {
            _isLoading = false;
            _error = response.Error;
            return;
        }
        else
        {
            try
            {
                var tempdata = response.Result!.Items.Where(pp => pp.IsDeleted == "false").ToList();
                _dataCount.AddRange(tempdata);
            }
            catch (Exception ex)
            {
                // Log and rethrow for any other unforeseen exceptions
                var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                //LogException(exceptionDetails);
                //throw;  // Rethrow the exception to let the calling code handle it if needed
            }

            try
            {
                _totalapps = _dataCount.Count;
                foreach (var item in _dataCount)
                {
                    try
                    {

                        if (string.IsNullOrEmpty(item.Bisnis_Process))
                        {
                            _totalappsbispro += 1;
                        }

                        if (string.IsNullOrEmpty(item.Capability_Level_1))
                        {
                            _totalappscapability += 1;
                        }

                        if (string.IsNullOrEmpty(item.Capability_Level_2))
                        {
                            _totalappscapability += 1;
                        }

                        if (string.IsNullOrEmpty(item.Application_Status))
                        {
                            _totalappsstatus += 1;
                        }

                        if (string.IsNullOrEmpty(item.Link_Application))
                        {
                            _totalappsurl += 1;
                        }

                        if (string.IsNullOrEmpty(item.Start_Implementation))
                        {
                            _totalgolive += 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log and rethrow for any other unforeseen exceptions
                        var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                        //LogException(exceptionDetails);
                        //throw;  // Rethrow the exception to let the calling code handle it if needed
                    }

                    try
                    {
                        if (string.IsNullOrEmpty(item.Application_Area))
                        {
                            _totalappsareakosong += 1;
                        }
                        else
                        {
                            if (item.Application_Area.ToLower().Contains("persero"))
                            {
                                _totalappsholding += 1;
                            }
                            else if (item.Application_Area.ToLower().Contains("head office"))
                            {
                                _totalappskpipusat += 1;
                            }
                            else
                            {
                                _totalappskpiru += 1;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        // Log and rethrow for any other unforeseen exceptions
                        var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                        //LogException(exceptionDetails);
                        //throw;  // Rethrow the exception to let the calling code handle it if needed
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and rethrow for any other unforeseen exceptions
                var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                //LogException(exceptionDetails);
                //throw;  // Rethrow the exception to let the calling code handle it if needed
            }

            _isLoading = false;
        }
    }
    public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
    {
        if (args.Item.Id == "Grid_excelexport") //Id is combination of Grid's ID and itemname test
        {
            var excelProperties = new ExcelExportProperties
            {
                FileName = "exporteacatalog " + System.DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx"
            };
            await _blazorDataGrid!.ExportToExcelAsync(excelProperties);
        }
    }

    public void RowBound(RowDataBoundEventArgs<GetSingleData> args)
    {
        if (!string.IsNullOrEmpty(args.Data.Application_Status))
        {
            if (args.Data.Application_Status.ToLower() == "aktif")
            {
                //args.Row.AddClass(new string[] { "status-aktif" });
            }
            else
            {
                args.Row.AddClass(new string[] { "status-nonaktif" });
            }
        }
        else
        {
            args.Row.AddClass(new string[] { "status-nonaktif" });
        }
    }
}
