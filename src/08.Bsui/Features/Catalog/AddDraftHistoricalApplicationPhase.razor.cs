//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Components;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Components.Forms;
//using MudBlazor;
//using Pertamina.SolutionTemplate.Bsui.Common.Constants;
//using Pertamina.SolutionTemplate.Bsui.Features.Catalog.Constants;
//using Pertamina.SolutionTemplate.Shared.Common.Constants;
//using Pertamina.SolutionTemplate.Shared.Common.Responses;
//using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
//using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;
//using Syncfusion.Blazor.DropDowns;
//using Syncfusion.Blazor.Data;
//using Syncfusion.Blazor.Calendars;

//namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog;

//public partial class AddDraftHistoricalApplicationPhase
//{
//    private ErrorResponse? _error;
//    private List<BreadcrumbItem> _breadcrumbItems = new();
//    private bool _isLoading;

//    private readonly AddDraftHistoricalApplicationPhaseRequest _request = new();
//    private readonly Type _models = typeof(GetSingleData);
//    private List<GetSingleData> _dataCount = default!;
//    private List<Phases> _dataPhase = default!;
//    private SfDropDownList<string, Phases>? _sfdropdownphase;
//    private SfAutoComplete<string, GetSingleData>? _sfapplicationame;
//    public bool Enabled_sfdropdownphase { get; set; } = false;
//    public bool Enabled_sfdropdownname { get; set; } = false;
//    public bool Highlight { get; set; } = true;
//    private bool Visibility { get; set; } = true;
//    private bool ShowButton { get; set; } = false;
//    private string? _strHeadDialog;
//    private string? _strContentDialog;
//    private string? _strLinkDialog;
//    private SfDatePicker<DateTime>? _dateStart;
//    [CascadingParameter]
//    private Task<AuthenticationState> AuthenticationStateTask { get; set; } = default!;
//    private ClaimsPrincipal _user = default!;
//    protected override async Task OnInitializedAsync()
//    {
//        SetupBreadcrumb();
//        _user = (await AuthenticationStateTask).User;
//        _request.Date = DateTime.Today;
//        _request.IsApproved = "Waiting For Approval";
//        _request.Source = "Web CRUD";
//        _request.Approved_Status = "Waiting For Approval";
//        _request.Created_By = _user.Identity.Name;
//        _request.IsDeleted = "false";
//        Enabled_sfdropdownphase = false;
//        Enabled_sfdropdownname = true;
//        Visibility = false;
//        await GetDatas();
//        _error = null;
//    }
//    public async void FocusHandlerStart(Syncfusion.Blazor.Calendars.FocusEventArgs args)
//    {
//        await _dateStart.ShowPopupAsync();
//    }
//    private async Task OnFilter(FilteringEventArgs args)
//    {
//        args.PreventDefaultAction = true;
//        var orWhere = WhereFilter.Or(new List<WhereFilter> {
//            new () { Field = "Application_Name", Operator = "contains", value = args.Text, IgnoreCase = true },
//            new () { Field = "Code_Apps", Operator = "contains", value = args.Text, IgnoreCase = true }
//        });
//        var query = new Query().Where(orWhere);
//        query = !string.IsNullOrEmpty(args.Text) ? query : new Query();
//        await _sfapplicationame.FilterAsync(_dataCount, query);
//    }
//    private void ValueChangeHandler(ChangeEventArgs<string, GetSingleData> args)
//    {
//        if (!string.IsNullOrEmpty(args.Value))
//        {
//            _request.Code_Apps_Update = args.ItemData.Code_Apps;
//            _request.Name_Apps_Update = args.ItemData.Application_Name;
//            if (args.ItemData.Application_Status == "Aktif")
//            {
//                _dataPhase = new List<Phases>();
//                _dataPhase.Add(new Phases
//                {
//                    ID = "0",
//                    Text = "Tidak Aktif"
//                });
//                _dataPhase.Add(new Phases
//                {
//                    ID = "2",
//                    Text = "Rasionalisasi"
//                });

//            }
//            else if (args.ItemData.Application_Status == "Tidak Aktif")
//            {
//                _dataPhase = new List<Phases>();
//                _dataPhase.Add(new Phases
//                {
//                    ID = "1",
//                    Text = "Aktif"
//                });
//                _dataPhase.Add(new Phases
//                {
//                    ID = "2",
//                    Text = "Rasionalisasi"
//                });
//            }
//            else
//            {
//                _dataPhase = new List<Phases>();
//                _dataPhase.Add(new Phases
//                {
//                    ID = "0",
//                    Text = "Tidak Aktif"
//                });
//                _dataPhase.Add(new Phases
//                {
//                    ID = "1",
//                    Text = "Aktif"
//                });
//            }

//            Enabled_sfdropdownphase = true;
//        }
//        else
//        {
//            _request.Phase = args.Value;
//            _dataPhase = new List<Phases>();
//            Enabled_sfdropdownphase = false;
//        }

//    }
//    private async Task OnFocus()
//    {
//        await _sfapplicationame.ShowPopupAsync();
//    }
//    private async Task GetDatas()
//    {
//        _dataCount = new List<GetSingleData>();
//        _isLoading = true;
//        var response = await _dataService.GetAllDataAsync();
//        if (response.Error is not null)
//        {
//            _isLoading = false;
//            _error = response.Error;
//            return;
//        }
//        else
//        {
//            _dataCount.AddRange(response.Result!.Items);
//            _isLoading = false;
//        }
//    }
//    private void SetupBreadcrumb()
//    {
//        _breadcrumbItems = new()
//        {
//            CommonBreadcrumbFor.Home,
//            BreadcrumbFor.Request,
//            CommonBreadcrumbFor.Active(CommonDisplayTextFor.AddDraftCatalog)
//        };
//    }
//    public void OnValueChange(ChangeEventArgs<string, GetSingleData> args)
//    {
//        if (args.ItemData.Application_Status == "Aktif")
//        {
//            _dataPhase = new List<Phases>();
//            _dataPhase.Add(new Phases
//            {
//                ID = "0",
//                Text = "Not Active"
//            });
//            _dataPhase.Add(new Phases
//            {
//                ID = "2",
//                Text = "Rationalization"
//            });
//        }
//        else if (args.ItemData.Application_Status == "Tidak Aktif")
//        {
//            _dataPhase = new List<Phases>();
//            _dataPhase.Add(new Phases
//            {
//                ID = "0",
//                Text = "Rationalization"
//            });
//            _dataPhase.Add(new Phases
//            {
//                ID = "1",
//                Text = "Active"
//            });

//        }
//        else
//        {
//            _dataPhase = new List<Phases>();
//            _dataPhase.Add(new Phases
//            {
//                ID = "0",
//                Text = "Active"
//            });
//            _dataPhase.Add(new Phases
//            {
//                ID = "1",
//                Text = "Not Active"
//            });

//        }

//        Enabled_sfdropdownphase = true;
//    }
//    public class Phases
//    {
//        public string ID { get; set; }
//        public string Text { get; set; }
//    }
//    private async Task OnValidSubmit(EditContext formContext)
//    {
//        Enabled_sfdropdownname = false;
//        var formIsValid = formContext.Validate();
//        if (formIsValid == false)
//        {
//            _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
//            _strContentDialog = "";
//            foreach (var item in formContext.GetValidationMessages())
//            {
//                _strContentDialog += item + "<br />";
//            }

//            _strLinkDialog = "";
//            Visibility = true;
//            return;
//        }

//        _error = null;

//        _isLoading = true;
//        _request.Day = _request.Date.Day;
//        _request.Year = _request.Date.Year;
//        _request.Month = _request.Date.Month;
//        var response = await _dataService.AddDraftHistoricalApplicationPhaseAsync(_request);
//        if (response.Error is not null)
//        {
//            _isLoading = false;
//            _strHeadDialog = "Pembuatan Draft Historical Aplikasi Phase Gagal";
//            _strContentDialog = "";
//            foreach (var item in response.Error.Details)
//            {
//                _strContentDialog += item + "<br />";
//            }

//            _strLinkDialog = "";
//            Visibility = true;
//            return;
//        }

//        _isLoading = false;
//        _strHeadDialog = "Pembuatan Draft Historical Aplikasi Phase Berhasil";
//        _strContentDialog = "app phase " + _request.Phase + " untuk kode aplikasi " + _request.Code_Apps + " Telah berhasil di tambah, dengan code draft sebagai berikut " + response.Result!.Code_App + " dan menunggu persetujuan";
//        _strLinkDialog = "ok";
//        Visibility = true;
//    }

//    private void OnBtnClick()
//    {
//        Visibility = true;
//        if (_strLinkDialog == "ok")
//        {
//            _navigationManager.NavigateTo(RouteFor.Request);
//        }
//        else
//        {
//            Enabled_sfdropdownname = true;
//        }
//    }
//    private void DialogOpen(object args)
//    {
//        ShowButton = false;
//    }
//    private void DialogClose(object args)
//    {
//        ShowButton = true;
//        if (_strLinkDialog == "ok")
//        {
//            _navigationManager.NavigateTo(RouteFor.Request);
//        }
//        else
//        {
//            Enabled_sfdropdownname = true;
//        }
//    }
//    private void OnInvalidSubmit(EditContext editContext)
//    {
//        Enabled_sfdropdownname = false;
//        _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
//        _strContentDialog = "";
//        foreach (var item in editContext.GetValidationMessages())
//        {
//            _strContentDialog += item + "<br />";
//        }

//        _strLinkDialog = "";
//        Visibility = true;
//    }
//}
