using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Common.Constants;
using Pertamina.SolutionTemplate.Bsui.Features.Catalog.Components;
using Pertamina.SolutionTemplate.Bsui.Features.Catalog.Models;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Extensions;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Commands.DetailData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog;

public partial class BuildingBlock
{
    private bool _isLoading;
    private ErrorResponse? _error;
    private List<AppCatalogGroupLevel1> _dataCount = default!;
    private List<GetSingleData> _dataCount1 = default!;
    private readonly List<BreadcrumbItem> _breadcrumbItems = new();
    private string _greetings = default!;

    protected override async Task OnInitializedAsync()
    {
        SetupBreadcrumb();

        _greetings = $"Good {DateTimeOffset.Now.ToFriendlyTimeDisplayText()}";
        await GetDatas();
    }
    private void SetupBreadcrumb()
    {
        _breadcrumbItems.Add(CommonBreadcrumbFor.Catalog);
        _breadcrumbItems.Add(CommonBreadcrumbFor.Active(CommonDisplayTextFor.BuildingBlock));
    }
    private async Task GetDatas()
    {
        _isLoading = true;
        _dataCount = new List<AppCatalogGroupLevel1>();
        _dataCount1 = new List<GetSingleData>();
        var response = await _dataService.GetAllDataAsync();
        if (response.Error is not null)
        {
            _error = response.Error;
            _isLoading = false;
            return;
        }
        else
        {
            try
            {
                var tempdata = response.Result!.Items.Where(pp => pp.IsDeleted == "false").ToList();
                _dataCount1.AddRange(tempdata);
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
                var tempdatagroup = new List<AppCatalogGroupLevel1>();
                var level1group = _dataCount1.GroupBy(pp => pp.Capability_Level_1).ToList();
                foreach (var itemlevel1 in level1group)
                {
                    try
                    {
                        if (itemlevel1.Count() > 0)
                        {
                            if (!string.IsNullOrEmpty(itemlevel1.Key))
                            {
                                var level_1_group = new AppCatalogGroupLevel1
                                {
                                    Level_1_WithDetails = itemlevel1.Key.ToUpper(),
                                    Level_1_lower_WithDetails = itemlevel1.Key,
                                    Level_2_WithDetails = new List<AppCatalogGroupLevel2>()
                                };
                                var level2group = itemlevel1.GroupBy(pp => pp.Capability_Level_2).ToList();
                                foreach (var itemlevel2 in level2group)
                                {
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(itemlevel2.Key))
                                        {
                                            var level_2_group = new AppCatalogGroupLevel2
                                            {
                                                Level_1_WithDetails = itemlevel1.Key.ToUpper(),
                                                Level_1_lower_WithDetails = itemlevel1.Key,
                                                Level_2_WithDetails = itemlevel2.Key.ToUpper(),
                                                Level_2_lower_WithDetails = itemlevel2.Key,
                                                Data_WithDetails = new List<AppCatalogGroupArea>()
                                            };
                                            var mastergroup = new List<AppCatalogGroupArea>();
                                            var areagroup = itemlevel2.GroupBy(pp => pp.Application_Area).ToList();
                                            foreach (var itemarea in areagroup)
                                            {
                                                var sarea = "";
                                                if (itemarea.Key.ToLower().Contains("persero"))
                                                {
                                                    sarea = "HOLDING";
                                                }
                                                else if (itemarea.Key.ToLower().Contains("head office"))
                                                {
                                                    sarea = "KPI HEAD OFFICE";
                                                }
                                                else
                                                {
                                                    sarea = "KPI REFINERY UNIT";
                                                }

                                                var itemlevel3 = new AppCatalogGroupArea
                                                {
                                                    Area = sarea,
                                                    Data = new List<AppCatalogGroupStatus>()
                                                };
                                                var statusgroup = itemarea.GroupBy(pp => pp.Application_Status).ToList();
                                                foreach (var itemstatus in statusgroup)
                                                {
                                                    var strtext = "";
                                                    if (itemstatus.Key == "Aktif")
                                                    {
                                                        strtext = "ACTIVE";
                                                    }
                                                    else if (itemstatus.Key == "Tidak Aktif")
                                                    {
                                                        strtext = "NOT ACTIVE";
                                                    }
                                                    else
                                                    {
                                                        strtext = "UKNOWN";
                                                    }

                                                    var itemlevel4 = new AppCatalogGroupStatus
                                                    {
                                                        Status = strtext,
                                                        Total = itemstatus.Count(),
                                                        Data = new List<GetSingleData>()
                                                    };
                                                    //strtext += itemstatus.Count();
                                                    itemlevel4.Status = strtext;
                                                    itemlevel4.Data.AddRange(itemstatus.ToList());
                                                    itemlevel3.Data.Add(itemlevel4);
                                                }

                                                mastergroup.Add(itemlevel3);

                                            }

                                            var groupbyarea = mastergroup.GroupBy(pp => pp.Area).ToList();
                                            foreach (var item in groupbyarea)
                                            {
                                                var itemlevel3 = new AppCatalogGroupArea
                                                {
                                                    Area = item.Key + ".",
                                                    Data = new List<AppCatalogGroupStatus>()
                                                };

                                                var totalapps = 0;
                                                var totalaktif = 0;
                                                var totalnonaktif = 0;
                                                var masterstatusgroup = new List<AppCatalogGroupStatus>();
                                                foreach (var subitem in item)
                                                {
                                                    var groupstatus = subitem.Data.GroupBy(pp => pp.Status).ToList();

                                                    foreach (var itemstatus in groupstatus)
                                                    {
                                                        masterstatusgroup.AddRange(itemstatus);
                                                    }

                                                }

                                                var groupbystatus = masterstatusgroup.GroupBy(pp => pp.Status).ToList();
                                                foreach (var subitem in groupbystatus)
                                                {
                                                    var itemlevel4 = new AppCatalogGroupStatus
                                                    {
                                                        Status = subitem.Key,
                                                        Data = new List<GetSingleData>()
                                                    };
                                                    if (subitem.Key == "ACTIVE")
                                                    {

                                                        foreach (var subitemstatus in subitem)
                                                        {
                                                            totalaktif += subitemstatus.Total;
                                                            itemlevel4.Data.AddRange(subitemstatus.Data);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        foreach (var subitemstatus in subitem)
                                                        {
                                                            totalnonaktif += subitemstatus.Total;
                                                            itemlevel4.Data.AddRange(subitemstatus.Data);
                                                        }

                                                    }

                                                    itemlevel3.Data.Add(itemlevel4);
                                                }

                                                try
                                                {
                                                    var groupbykey4 = itemlevel3.Data.GroupBy(pp => pp.Status).ToList();
                                                    foreach (var itemlevel4 in groupbykey4)
                                                    {
                                                        var strkey = itemlevel4.Key;
                                                        foreach (var itemkey in itemlevel4)
                                                        {
                                                            var inttotal = 0;
                                                            foreach (var subitem in itemkey.Data)
                                                            {
                                                                inttotal += 1;
                                                            }

                                                            strkey += " " + inttotal;
                                                            itemkey.Status = strkey;
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

                                                var orderlist = itemlevel3.Data.OrderBy(pp => pp.Status).ToList();
                                                itemlevel3.Data = orderlist;
                                                totalapps = totalaktif + totalnonaktif;
                                                if (totalapps != 0)
                                                {
                                                    itemlevel3.Area += " TOTAL " + totalapps;
                                                }

                                                if (totalaktif != 0)
                                                {
                                                    itemlevel3.Area += ", ACTIVE " + totalaktif;
                                                }

                                                if (totalnonaktif != 0)
                                                {
                                                    itemlevel3.Area += ", NOT ACTIVE " + totalnonaktif;
                                                }

                                                level_2_group.Data_WithDetails.Add(itemlevel3);
                                            }

                                            var level2order = level_2_group.Data_WithDetails.OrderBy(pp => pp.Area);
                                            level_2_group.Data_WithDetails = level2order.ToList();
                                            level_1_group.Level_2_WithDetails.Add(level_2_group);
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

                                tempdatagroup.Add(level_1_group);
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

                _dataCount = tempdatagroup.OrderBy(pp => pp.Level_1_WithDetails).ToList();
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
    private async Task ShowDialog(string strapp, string level1)
    {
        var request = new DetailDataRequest();

        try
        {
            var foundata = _dataCount1.Where(pp => pp.Application_Name == strapp && pp.Capability_Level_1 == level1).ToList();
            request.Appdesc = foundata.FirstOrDefault().Description;
            request.Appowner = foundata.FirstOrDefault().Business_Owner_PIC;
            request.Appownerpic = foundata.FirstOrDefault().Business_Owner_PIC;
            request.Appownerdev = foundata.FirstOrDefault().Developer;
            request.Applink = foundata.FirstOrDefault().Link_Application;
            if (string.IsNullOrEmpty(request.Applink))
            {
                request.Applink = "#";
            }
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

