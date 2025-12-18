using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Common.Constants;
using Pertamina.SolutionTemplate.Bsui.Features.Catalog.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetAllMasterData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Model;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Globalization;
using System.Security.Claims;

namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog;

public partial class EditCatalog
{
    private ErrorResponse? _error;
    private List<BreadcrumbItem> _breadcrumbItems = new();
    private bool _isLoading;
    private bool _isLoadingIdaman = false;
    private readonly AddDraftCatalogRequest _request = new();
    private readonly Type _models = typeof(GetSingleData);
    private List<GetSingleData> _dataCount = default!;
    private SfAutoComplete<string, GetSingleData>? _sfapplicationame;
    private List<GetAllMasterData> _dataCountArea = default!;
    private List<GetAllMasterData> _dataCountStatus = default!;
    private List<GetAllMasterData> _dataCountUsman = default!;
    private List<GetAllMasterData> _dataCountType = default!;
    private List<IdamanUsers> _dataCountbisuserwithkbo = default!;
    private SfAutoComplete<string, GetAllMasterData>? _sfapparea;
    private SfAutoComplete<string, GetAllMasterData>? _sfapptype;
    private SfAutoComplete<string, GetAllMasterData>? _sfappstatus;
    private SfAutoComplete<string, GetAllMasterData>? _sfappusman;
    public bool Highlight { get; set; } = true;
    public bool Disable_sfdropdownphase { get; set; } = false;
    public bool Enabled_sfdropdownphase { get; set; } = false;
    public bool Enabled_sfdropdownname { get; set; } = false;
    private bool Visibility { get; set; } = true;
    private bool VisibilityGrid { get; set; } = true;
    private bool ShowButton { get; set; } = false;
    private string? _strHeadDialog;
    private string? _strContentDialog;
    private string? _strLinkDialog;
    private SfDatePicker<DateTime>? _dateStartImplement;
    private SfDatePicker<DateTime>? _dateStartDev;
    //private SfDatePicker<DateTime>? _dateStartRationalize;
    //private SfDatePicker<DateTime>? _dateStartSunset;
    private SfGrid<IdamanUsers>? _blazorDataGridbisuser;
    //private bool _isCheckedSunset = false;
    //private bool _isCheckedRationalize = false;
    private string _sinputbisnisuseridaman = "";
    private string _stypedialog = "";
    private string _strerrordialogidaman = "";
    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationStateTask { get; set; } = default!;
    private ClaimsPrincipal _user = default!;
    protected override async Task OnInitializedAsync()
    {
        SetupBreadcrumb();
        _user = (await AuthenticationStateTask).User;
        _request.Start_Development = DateTime.Today;
        _request.Start_Implementation = DateTime.Today;
        _request.Created_By = _user.Identity.Name;
        _request.IsApproved = "Waiting For Approval";
        _request.Source = "Web CRUD";
        _request.Approved_Status = "Waiting For Approval";
        Enabled_sfdropdownphase = true;
        Disable_sfdropdownphase = false;
        Enabled_sfdropdownname = true;
        Visibility = false;
        VisibilityGrid = false;
        _dataCountbisuserwithkbo = new List<IdamanUsers>();
        await GetDatas();
        await GetMasterDataAll();

        _error = null;
    }
    public async void FocusHandlerStartImplement(Syncfusion.Blazor.Calendars.FocusEventArgs args)
    {
        await _dateStartImplement.ShowPopupAsync();
    }
    public async void FocusHandlerStartDev(Syncfusion.Blazor.Calendars.FocusEventArgs args)
    {
        await _dateStartDev.ShowPopupAsync();
    }
    private async Task OnFilter(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        var orWhere = WhereFilter.Or(new List<WhereFilter> {
            new () { Field = "Application_Name", Operator = "contains", value = args.Text, IgnoreCase = true },
            new () { Field = "Code_Apps", Operator = "contains", value = args.Text, IgnoreCase = true }
        });
        var query = new Query().Where(orWhere);
        query = !string.IsNullOrEmpty(args.Text) ? query : new Query();
        await _sfapplicationame.FilterAsync(_dataCount, query);
    }
    private async Task OnFocusAppName()
    {
        await _sfapplicationame.ShowPopupAsync();
    }
    private async Task OnFocusArea()
    {
        await _sfapparea.ShowPopupAsync();
    }
    private async Task OnFocusType()
    {
        await _sfapptype.ShowPopupAsync();
    }
    private async Task OnFocusStatus()
    {
        await _sfappstatus.ShowPopupAsync();
    }
    private async Task OnFocusUsman()
    {
        await _sfappusman.ShowPopupAsync();
    }
    private async Task GetMasterDataAll()
    {
        _dataCountArea = new List<GetAllMasterData>();
        _dataCountStatus = new List<GetAllMasterData>();
        _dataCountUsman = new List<GetAllMasterData>();
        _dataCountType = new List<GetAllMasterData>();
        _isLoading = true;
        var response = await _dataService.GetTableMasterDataAsync();
        if (response.Error is not null)
        {
            _isLoading = false;
            _error = response.Error;
            return;
        }
        else
        {
            var groupbytable = response.Result!.Items.GroupBy(pp => pp.Table_Name).ToList();
            foreach (var item in groupbytable)
            {
                if (item.Key == "Area")
                {
                    _dataCountArea.AddRange(item);
                }
                else if (item.Key == "Status")
                {
                    _dataCountStatus.AddRange(item);
                }
                else if (item.Key == "User Management")
                {
                    _dataCountUsman.AddRange(item);
                }
                else if (item.Key == "Type")
                {
                    _dataCountType.AddRange(item);
                }
            }

            _isLoading = false;
        }
    }

    private async Task OnFilterArea(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        var orWhere = WhereFilter.Or(new List<WhereFilter> {
            new () { Field = "Nama", Operator = "contains", value = args.Text, IgnoreCase = true },
            new () { Field = "Id", Operator = "contains", value = args.Text, IgnoreCase = true }
        });
        var query = new Query().Where(orWhere);
        query = !string.IsNullOrEmpty(args.Text) ? query : new Query();
        await _sfapparea.FilterAsync(_dataCountArea, query);
    }
    private async Task OnFilterType(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        var orWhere = WhereFilter.Or(new List<WhereFilter> {
            new () { Field = "Nama", Operator = "contains", value = args.Text, IgnoreCase = true },
            new () { Field = "Id", Operator = "contains", value = args.Text, IgnoreCase = true }
        });
        var query = new Query().Where(orWhere);
        query = !string.IsNullOrEmpty(args.Text) ? query : new Query();
        await _sfapptype.FilterAsync(_dataCountType, query);
    }
    private async Task OnFilterStatus(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        var orWhere = WhereFilter.Or(new List<WhereFilter> {
            new () { Field = "Nama", Operator = "contains", value = args.Text, IgnoreCase = true },
            new () { Field = "Id", Operator = "contains", value = args.Text, IgnoreCase = true }
        });
        var query = new Query().Where(orWhere);
        query = !string.IsNullOrEmpty(args.Text) ? query : new Query();
        await _sfappstatus.FilterAsync(_dataCountStatus, query);
    }
    private async Task OnFilterUsman(FilteringEventArgs args)
    {
        args.PreventDefaultAction = true;
        var orWhere = WhereFilter.Or(new List<WhereFilter> {
            new () { Field = "Nama", Operator = "contains", value = args.Text, IgnoreCase = true },
            new () { Field = "Id", Operator = "contains", value = args.Text, IgnoreCase = true }
        });
        var query = new Query().Where(orWhere);
        query = !string.IsNullOrEmpty(args.Text) ? query : new Query();
        await _sfappusman.FilterAsync(_dataCountUsman, query);
    }
    private void ValueChangeHandler(ChangeEventArgs<string, GetSingleData> args)
    {
        if (!string.IsNullOrEmpty(args.Value))
        {
            try
            {
                _request.Application_Name = args.ItemData.Application_Name;
                _request.Description = args.ItemData.Description;
                _request.Application_Area = args.ItemData.Application_Area;
                _request.Application_Type = args.ItemData.Application_Type;
                _request.Application_Status = args.ItemData.Application_Status;
                _request.User_Management_Integration = args.ItemData.User_Management_Integration;
                _request.Business_Owner_Nama = args.ItemData.Business_Owner_Nama;
                _request.Business_Owner_Email = args.ItemData.Business_Owner_Email;
                _request.Business_Owner_KBO = args.ItemData.Business_Owner_KBO;
                _request.Business_Owner_Jabatan = args.ItemData.Business_Owner_Jabatan;
                _request.Business_Owner_PIC = args.ItemData.Business_Owner_PIC;
                _request.Business_Owner_PIC_Email = args.ItemData.Business_Owner_PIC_Email;
                _request.Developer = args.ItemData.Developer;
                _request.Business_Analyst = args.ItemData.Business_Analyst;
                try
                {
                    var convdate = Convert.ToDateTime(args.ItemData.Start_Development);
                    _request.Start_Development = convdate;
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
                    var convdate = Convert.ToDateTime(args.ItemData.Start_Development);
                    _request.Start_Implementation = convdate;
                }
                catch (Exception ex)
                {
                    // Log and rethrow for any other unforeseen exceptions
                    var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                    //LogException(exceptionDetails);
                    //throw;  // Rethrow the exception to let the calling code handle it if needed
                }

                //if (args.ItemData.IsDeleted == "false")
                //{
                //    if (args.ItemData.Application_Status == "")
                //    {
                //        _isCheckedRationalize = true;
                //    }
                //}

                //if (args.ItemData.IsDeleted == "true")
                //{
                //    _isCheckedSunset = true;
                //}

                _request.IsDeleted = args.ItemData.IsDeleted;
                Enabled_sfdropdownphase = false;
                Disable_sfdropdownphase = true;
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
            _dataCount.AddRange(response.Result!.Items);
            _isLoading = false;
        }
    }
    private void SetupBreadcrumb()
    {
        _breadcrumbItems = new()
        {
            CommonBreadcrumbFor.Home,
            BreadcrumbFor.Request,
            CommonBreadcrumbFor.Active(CommonDisplayTextFor.Edit)
        };
    }
    private async Task OnShowDialogIdaman(string stype)
    {
        _stypedialog = stype;
        _sinputbisnisuseridaman = "";

        if (_dataCountbisuserwithkbo.Any())
        {
            _dataCountbisuserwithkbo.Clear();
            await _blazorDataGridbisuser.Refresh();
        }

        VisibilityGrid = true;
    }

    private async Task OnBindIdaman(string stype)
    {
        _strerrordialogidaman = "";
        _isLoadingIdaman = true;
        if (_dataCountbisuserwithkbo.Any())
        {
            _dataCountbisuserwithkbo.Clear();
            await _blazorDataGridbisuser.Refresh();
        }

        try
        {
            var datarestResponse = await _dataService.GetUserFromIdaman(_idamaninfo.Value.BaseUrl,
                stype, _stypedialog, $"{JwtBearerDefaults.AuthenticationScheme} {_userInfo.AccessToken}");
            _dataCountbisuserwithkbo.AddRange(datarestResponse);
            //var restClient = new RestClient(_idamaninfo.Value.BaseUrl);
            //var restRequest = new RestRequest("/v1/users?searchText=" + stype, Method.Get);
            //restRequest.AddHeader(HttpHeaderName.Authorization, $"{JwtBearerDefaults.AuthenticationScheme} {_userInfo.AccessToken}");

            //var restResponse = await restClient.ExecuteAsync(restRequest);

            //var data = JsonSerializer.Deserialize<MasterJsonIdamanUsers>(restResponse)!;
            //foreach (var item in datarestResponse)
            //{
            //    if (_stypedialog == "bisuserkbo")
            //    {
            //        if (!string.IsNullOrEmpty(item.position.kbo))
            //        {
            //            _dataCountbisuserwithkbo.Add(item);
            //        }
            //    }
            //    else
            //    {
            //        _dataCountbisuserwithkbo.Add(item);
            //    }
            //}

            await _blazorDataGridbisuser.Refresh();
        }
        catch (Exception ex)
        {
            _strerrordialogidaman = "Loading Data From Idaman Failed";
        }
        _isLoadingIdaman = false;
    }
    private void CommandClickGridbisuser(CommandClickEventArgs<IdamanUsers> args)
    {
        if (_stypedialog == "bisusernonkboanalyst")
        {
            _request.Business_Analyst = args.RowData.displayName;
        }
        else if (_stypedialog == "bisusernonkbopic")
        {
            _request.Business_Owner_PIC = args.RowData.displayName;
            _request.Business_Owner_PIC_Email = args.RowData.email;
        }
        else if (_stypedialog == "bisuserkbo")
        {
            _request.Business_Owner_Nama = args.RowData.displayName;
            _request.Business_Owner_Email = args.RowData.email;
            _request.Business_Owner_KBO = args.RowData.position.kbo;
            _request.Business_Owner_Jabatan = args.RowData.jobTitle;
        }

        //bisusernonkboanalyst,bisusernonkbopic,bisuserkbo
        VisibilityGrid = false;
    }
    public async Task OnClickBtnUpdateAndWaiting(EditContext formContext)
    {
        Enabled_sfdropdownname = false;
        var formIsValid = formContext.Validate();
        if (formIsValid == false)
        {
            _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
            _strContentDialog = "";
            foreach (var item in formContext.GetValidationMessages())
            {
                _strContentDialog += item + "<br />";
            }

            _strLinkDialog = "";
            Visibility = true;
            return;
        }

        _error = null;
        _isLoading = true;
        _request.Tipe_Request = "Update";
        _request.Company_Code = "2186";
        _request.IsDeleted = "false";

        try
        {
            var convertstr = _request.Start_Development.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Development_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
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
            var convertstr = _request.Start_Implementation.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Implementation_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
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
            var convertstr = _request.Start_Rationalization.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Rationalization_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
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
            var convertstr = _request.Start_Sunset.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Sunset_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        //if (_isCheckedSunset)
        //{
        //    if (_request.Start_Sunset.ToString().Contains("0001"))
        //    {
        //        _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
        //        _strContentDialog = "Field Start Sunset";
        //        _strLinkDialog = "";
        //        Visibility = true;
        //        _isLoading = false;
        //        return;
        //    }

        //    _request.IsDeleted = "true";
        //    _request.Application_Status = "Tidak Aktif";
        //}
        //else
        //{
        //    _request.IsDeleted = "false";
        //}

        //if (_isCheckedRationalize)
        //{
        //    if (_request.Start_Rationalization.ToString().Contains("0001"))
        //    {
        //        _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
        //        _strContentDialog = "Field Start Rationalization";
        //        _strLinkDialog = "";
        //        Visibility = true;
        //        _isLoading = false;
        //        return;
        //    }

        //    _request.IsDeleted = "false";
        //    _request.Application_Status = "";
        //}

        var response = await _dataService.AddDraftCatalogAsync(_request);
        if (response.Error is not null)
        {

            if (response.Error.Detail.ToLower().Contains("already exist"))
            {
                _strHeadDialog = "Pembuatan Draft Edit App Catalog Gagal";
                _strContentDialog = "Mohon gunakan Nama Aplikasi yang lain, karena sudah di gunakan oleh aplikasi yang lain";
                _strLinkDialog = "";
                _request.Application_Name = "";
            }
            else
            {
                _strHeadDialog = "Pembuatan Draft Edit App Catalog Gagal";
                _strContentDialog = "";
                foreach (var item in response.Error.Details)
                {
                    if (item.ToLower().Contains("already exist"))
                    {
                        _request.Application_Name = "";
                    }

                    _strContentDialog += item + "<br />";
                }

                _strLinkDialog = "";
            }

            _isLoading = false;
            Visibility = true;
            return;
        }

        _isLoading = false;
        _strHeadDialog = "Pembuatan Draft Edit App Catalog Berhasil";
        _strContentDialog = "Nama Aplikasi " + _request.Application_Name + " Telah berhasil di tambah, dengan code draft sebagai berikut " + response.Result!.Code_App + " dan menunggu persetujuan";
        _strLinkDialog = "ok";
        Visibility = true;
    }
    public async Task OnClickBtnSunsesAndWaiting(EditContext formContext)
    {
        Enabled_sfdropdownname = false;
        var formIsValid = formContext.Validate();
        if (formIsValid == false)
        {
            _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
            _strContentDialog = "";
            foreach (var item in formContext.GetValidationMessages())
            {
                _strContentDialog += item + "<br />";
            }

            _strLinkDialog = "";
            Visibility = true;
            return;
        }

        _error = null;
        _isLoading = true;
        _request.Tipe_Request = "Update";
        _request.Company_Code = "2186";
        _request.IsDeleted = "true";
        _request.Application_Status = "";
        try
        {
            var convertstr = _request.Start_Development.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Development_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
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
            var convertstr = _request.Start_Implementation.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Implementation_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        //try
        //{
        //    var convertstr = _request.Start_Rationalization.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //    var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        //    _request.Start_Rationalization_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //}
        //catch
        //{

        //}

        var response = await _dataService.AddDraftCatalogAsync(_request);
        if (response.Error is not null)
        {

            if (response.Error.Detail.ToLower().Contains("already exist"))
            {
                _strHeadDialog = "Pembuatan Draft Edit App Catalog Gagal";
                _strContentDialog = "Mohon gunakan Nama Aplikasi yang lain, karena sudah di gunakan oleh aplikasi yang lain";
                _strLinkDialog = "";
                _request.Application_Name = "";
            }
            else
            {
                _strHeadDialog = "Pembuatan Draft Edit App Catalog Gagal";
                _strContentDialog = "";
                foreach (var item in response.Error.Details)
                {
                    if (item.ToLower().Contains("already exist"))
                    {
                        _request.Application_Name = "";
                    }

                    _strContentDialog += item + "<br />";
                }

                _strLinkDialog = "";
            }

            _isLoading = false;
            Visibility = true;
            return;
        }

        _isLoading = false;
        _strHeadDialog = "Pembuatan Draft Edit App Catalog Berhasil";
        _strContentDialog = "Nama Aplikasi " + _request.Application_Name + " Telah berhasil di tambah, dengan code draft sebagai berikut " + response.Result!.Code_App + " dan menunggu persetujuan";
        _strLinkDialog = "ok";
        Visibility = true;
    }
    public async Task OnClickBtnActiveAndWaiting(EditContext formContext)
    {
        Enabled_sfdropdownname = false;
        var formIsValid = formContext.Validate();
        if (formIsValid == false)
        {
            _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
            _strContentDialog = "";
            foreach (var item in formContext.GetValidationMessages())
            {
                _strContentDialog += item + "<br />";
            }

            _strLinkDialog = "";
            Visibility = true;
            return;
        }

        _error = null;
        _isLoading = true;
        _request.Tipe_Request = "Update";
        _request.Company_Code = "2186";
        _request.IsDeleted = "false";
        _request.Application_Status = "Aktif";
        try
        {
            var convertstr = _request.Start_Development.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Development_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
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
            var convertstr = _request.Start_Implementation.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Implementation_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        var response = await _dataService.AddDraftCatalogAsync(_request);
        if (response.Error is not null)
        {

            if (response.Error.Detail.ToLower().Contains("already exist"))
            {
                _strHeadDialog = "Pembuatan Draft Edit App Catalog Gagal";
                _strContentDialog = "Mohon gunakan Nama Aplikasi yang lain, karena sudah di gunakan oleh aplikasi yang lain";
                _strLinkDialog = "";
                _request.Application_Name = "";
            }
            else
            {
                _strHeadDialog = "Pembuatan Draft Edit App Catalog Gagal";
                _strContentDialog = "";
                foreach (var item in response.Error.Details)
                {
                    if (item.ToLower().Contains("already exist"))
                    {
                        _request.Application_Name = "";
                    }

                    _strContentDialog += item + "<br />";
                }

                _strLinkDialog = "";
            }

            _isLoading = false;
            Visibility = true;
            return;
        }

        _isLoading = false;
        _strHeadDialog = "Pembuatan Draft Edit App Catalog Berhasil";
        _strContentDialog = "Nama Aplikasi " + _request.Application_Name + " Telah berhasil di tambah, dengan code draft sebagai berikut " + response.Result!.Code_App + " dan menunggu persetujuan";
        _strLinkDialog = "ok";
        Visibility = true;
    }
    public async Task OnClickBtnRationalizeAndWaiting(EditContext formContext)
    {
        Enabled_sfdropdownname = false;
        var formIsValid = formContext.Validate();
        if (formIsValid == false)
        {
            _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
            _strContentDialog = "";
            foreach (var item in formContext.GetValidationMessages())
            {
                _strContentDialog += item + "<br />";
            }

            _strLinkDialog = "";
            Visibility = true;
            return;
        }

        _error = null;
        _isLoading = true;
        _request.Tipe_Request = "Update";
        _request.Company_Code = "2186";
        _request.IsDeleted = "false";
        _request.Application_Status = "Rationalize";
        try
        {
            var convertstr = _request.Start_Development.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Development_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
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
            var convertstr = _request.Start_Implementation.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
            _request.Start_Implementation_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        catch (Exception ex)
        {
            // Log and rethrow for any other unforeseen exceptions
            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
            //LogException(exceptionDetails);
            //throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        //try
        //{
        //    var convertstr = _request.Start_Rationalization.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //    var convertdt = DateTime.ParseExact(convertstr, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        //    _request.Start_Rationalization_Str = convertdt.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //}
        //catch
        //{

        //}

        var response = await _dataService.AddDraftCatalogAsync(_request);
        if (response.Error is not null)
        {

            if (response.Error.Detail.ToLower().Contains("already exist"))
            {
                _strHeadDialog = "Pembuatan Draft Edit App Catalog Gagal";
                _strContentDialog = "Mohon gunakan Nama Aplikasi yang lain, karena sudah di gunakan oleh aplikasi yang lain";
                _strLinkDialog = "";
                _request.Application_Name = "";
            }
            else
            {
                _strHeadDialog = "Pembuatan Draft Edit App Catalog Gagal";
                _strContentDialog = "";
                foreach (var item in response.Error.Details)
                {
                    if (item.ToLower().Contains("already exist"))
                    {
                        _request.Application_Name = "";
                    }

                    _strContentDialog += item + "<br />";
                }

                _strLinkDialog = "";
            }

            _isLoading = false;
            Visibility = true;
            return;
        }

        _isLoading = false;
        _strHeadDialog = "Pembuatan Draft Edit App Catalog Berhasil";
        _strContentDialog = "Nama Aplikasi " + _request.Application_Name + " Telah berhasil di tambah, dengan code draft sebagai berikut " + response.Result!.Code_App + " dan menunggu persetujuan";
        _strLinkDialog = "ok";
        Visibility = true;
    }

    private void OnBtnClick()
    {
        Visibility = true;
        if (_strLinkDialog == "ok")
        {
            _navigationManager.NavigateTo(RouteFor.Request);
        }
    }
    private void DialogOpen(object args)
    {
        ShowButton = false;
    }
    private void DialogClose(object args)
    {
        ShowButton = true;
        if (_strLinkDialog == "ok")
        {
            _navigationManager.NavigateTo(RouteFor.Request);
        }
        else
        {
            Enabled_sfdropdownname = true;
        }
    }
    private void DialogOpenGrid(object args)
    {

    }
    private void DialogCloseGrid(object args)
    {
        VisibilityGrid = false;
    }
    private void OnInvalidSubmit(EditContext editContext)
    {
        Enabled_sfdropdownname = false;
        _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
        _strContentDialog = "";
        foreach (var item in editContext.GetValidationMessages())
        {
            _strContentDialog += item + "<br />";
        }

        _strLinkDialog = "";
        Visibility = true;
    }
}
