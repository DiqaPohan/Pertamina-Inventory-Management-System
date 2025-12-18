//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Forms;
//using MudBlazor;
//using Pertamina.SolutionTemplate.Bsui.Common.Constants;
//using Pertamina.SolutionTemplate.Bsui.Features.Catalog.Constants;
//using Pertamina.SolutionTemplate.Shared.Common.Constants;
//using Pertamina.SolutionTemplate.Shared.Common.Responses;
//using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
//using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;
//using Syncfusion.Blazor.DropDowns;

//namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog;

//public partial class ViewDraftHistoricalApplicationPhase
//{
//    [Parameter]
//    public string? Id { get; set; }
//    private ErrorResponse? _error;
//    private List<BreadcrumbItem> _breadcrumbItems = new();
//    private bool _isLoading;

//    private readonly AddDraftHistoricalApplicationPhaseRequest _request = new();
//    private bool Visibility { get; set; } = true;
//    private bool ShowButton { get; set; } = false;
//    private string? _strHeadDialog;
//    private string? _strContentDialog;
//    private string? _strLinkDialog;
//    protected override async Task OnInitializedAsync()
//    {
//        SetupBreadcrumb();
//        Visibility = false;
//        await GetDatas();
//        _error = null;
//    }
//    private async Task GetDatas()
//    {
//        _isLoading = true;
//        var response = await _dataService.GetAllDraftHistoricalApplicationPhaseDataAsync();
//        if (response.Error is not null)
//        {
//            _isLoading = false;
//            _error = response.Error;
//            return;
//        }
//        else
//        {
//            try
//            {
//                var foundata = response.Result!.Items.Where(pp => pp.Code_Apps == Id).FirstOrDefault();
//                _request.Code_Apps = foundata.Code_Apps;
//                try
//                {
//                    _request.Date = Convert.ToDateTime(foundata.Date.ToString());
//                }
//                catch
//                {

//                }

//                _request.Code_Apps_Update = foundata.Code_Apps_Update;
//                _request.Name_Apps_Update = foundata.Name_Apps_Update;
//                _request.Phase = foundata.Phase;
//                _request.Keterangan = foundata.Keterangan;
//                _request.IsApproved = foundata.IsApproved;
//                _request.Created_By = foundata.CreatedBy;
//                _request.Source = foundata.Source;
//                _request.IsDeleted = foundata.IsDeleted;
//                _request.Reason = foundata.Reason;
//            }
//            catch
//            {

//            }

//            _isLoading = false;
//        }
//    }
//    private void SetupBreadcrumb()
//    {
//        _breadcrumbItems = new()
//        {
//            CommonBreadcrumbFor.Home,
//            BreadcrumbFor.Request,
//            CommonBreadcrumbFor.Active(CommonDisplayTextFor.ViewDraftHistoricalPhase)
//        };
//    }
//    private void OnValueSelecthandler(SelectEventArgs<GetSingleData> args)
//    {
//        args.E.ToString();
//    }

//    public async Task OnClickBtnApproved(EditContext formContext)
//    {
//        var formIsValid = formContext.Validate();
//        if (formIsValid == false)
//        {
//            return;
//        }

//        _error = null;

//        _isLoading = true;

//        _request.IsApproved = "Approved";
//        _request.Approved_Status = "Approved";
//        _request.Reason = "";
//        var response = await _dataService.UpdateDraftHistoricalApplicationPhaseAsync(_request);

//        if (response.Error is not null)
//        {
//            //_error = response.Error;
//            _isLoading = false;
//            _strHeadDialog = "Proses Approved Draft Historical Aplikasi Gagal";
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
//        _strHeadDialog = "Draft Historical Aplikasi Berhasil di Approved";
//        _strContentDialog = "Code Draft " + _request.Code_Apps + " Telah di approved";
//        _strLinkDialog = "ok";
//        Visibility = true;

//    }
//    public async Task OnClickBtnRejected(EditContext formContext)
//    {
//        var formIsValid = formContext.Validate();
//        if (formIsValid == false)
//        {
//            return;
//        }

//        _error = null;

//        _isLoading = true;

//        _request.IsApproved = "Rejected";
//        _request.Approved_Status = "Rejected";
//        var response = await _dataService.UpdateDraftHistoricalApplicationPhaseAsync(_request);

//        if (response.Error is not null)
//        {
//            _isLoading = false;
//            _strHeadDialog = "Proses Rejected Draft Historical Aplikasi Gagal";
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
//        _strHeadDialog = "Draft Historical Aplikasi Di Tolak";
//        _strContentDialog = "Code Draft " + _request.Code_Apps + " di tolak";
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
//    }
//    private void OnInvalidSubmit(EditContext editContext)
//    {
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
