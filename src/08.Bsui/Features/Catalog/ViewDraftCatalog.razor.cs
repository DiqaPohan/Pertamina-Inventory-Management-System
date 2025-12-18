using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Common.Constants;
using Pertamina.SolutionTemplate.Bsui.Features.Catalog.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;
using Syncfusion.Blazor.DropDowns;

namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog;

public partial class ViewDraftCatalog
{
    [Parameter]
    public string? Id { get; set; }
    private ErrorResponse? _error;
    private List<BreadcrumbItem> _breadcrumbItems = new();
    private bool _isLoading;

    private readonly AddDraftCatalogRequest _request = new();
    private bool Visibility { get; set; } = true;
    private bool ShowButton { get; set; } = false;
    private string? _strHeadDialog;
    private string? _strContentDialog;
    private string? _strLinkDialog;
    public class ListByDiff
    {
        public string Field { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
    private List<ListByDiff> _listDiff = new();
    protected override async Task OnInitializedAsync()
    {
        SetupBreadcrumb();
        Visibility = false;
        await GetDatas();
        _error = null;
    }

    private void SetupBreadcrumb()
    {
        _breadcrumbItems = new()
        {
            CommonBreadcrumbFor.Home,
            BreadcrumbFor.Request,
            CommonBreadcrumbFor.Active(CommonDisplayTextFor.ViewDraftCatalog)
        };
    }
    private async Task GetDatas()
    {
        _isLoading = true;
        _listDiff = new List<ListByDiff>();
        var response = await _dataService.GetAllRequestDataAsync();
        var responsedata = await _dataService.GetAllDataAsync();
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
                var foundata = response.Result!.Items.Where(pp => pp.Code_Apps == Id).FirstOrDefault();
                _request.Code_Apps = foundata.Code_Apps;
                _request.Tipe_Request = foundata.Tipe_Request;
                _request.Code_Apps_Update = foundata.Code_Apps_Update;
                _request.Application_Name = foundata.Application_Name;
                _request.Description = foundata.Description;
                _request.Application_Area = foundata.Application_Area;
                _request.Application_Type = foundata.Application_Type;
                _request.Application_Status = foundata.Application_Status;
                _request.User_Management_Integration = foundata.User_Management_Integration;
                _request.Business_Owner_Nama = foundata.Business_Owner_Nama;
                _request.Business_Owner_Email = foundata.Business_Owner_Email;
                _request.Business_Owner_KBO = foundata.Business_Owner_Kbo;
                _request.Business_Owner_Jabatan = foundata.Business_Owner_Jabatan;
                _request.Business_Owner_PIC = foundata.Business_Owner_Pic;
                _request.Business_Owner_PIC_Email = foundata.Business_Owner_Pic_Email;
                _request.Developer = foundata.Developer;
                _request.Business_Analyst = foundata.Business_Analyst;
                try
                {
                    var convdate = Convert.ToDateTime(foundata.Start_Development);
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
                    var convdate = Convert.ToDateTime(foundata.Start_Implementation);
                    _request.Start_Implementation = convdate;
                }
                catch (Exception ex)
                {
                    // Log and rethrow for any other unforeseen exceptions
                    var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                    //LogException(exceptionDetails);
                    //throw;  // Rethrow the exception to let the calling code handle it if needed
                }

                _request.Company_Code = foundata.Company_Code;
                _request.Source = foundata.Source;
                _request.IsApproved = foundata.IsApproved;
                _request.Created_By = foundata.CreatedBy;
                _request.IsDeleted = foundata.IsDeleted;
                _request.Reason = foundata.Reason;
            }
            catch (Exception ex)
            {
                // Log and rethrow for any other unforeseen exceptions
                var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                //LogException(exceptionDetails);
                //throw;  // Rethrow the exception to let the calling code handle it if needed
            }

            if (_request.Tipe_Request == "Update")
            {
                try
                {
                    var foundata = responsedata.Result!.Items.Where(pp => pp.Code_Apps == _request.Code_Apps_Update).ToList();
                    if (foundata.Count > 0)
                    {
                        var firstfound = foundata.FirstOrDefault();
                        var iteminsert1 = new ListByDiff
                        {
                            Field = "App Name",
                            OldValue = firstfound.Application_Name,
                            NewValue = _request.Application_Name
                        };
                        var iteminsert2 = new ListByDiff
                        {
                            Field = "Description",
                            OldValue = firstfound.Description,
                            NewValue = _request.Description
                        };
                        var iteminsert3 = new ListByDiff
                        {
                            Field = "Area",
                            OldValue = firstfound.Application_Area,
                            NewValue = _request.Application_Area
                        };
                        var iteminsert4 = new ListByDiff
                        {
                            Field = "Type",
                            OldValue = firstfound.Application_Type,
                            NewValue = _request.Application_Type
                        };
                        var iteminsert5 = new ListByDiff
                        {
                            Field = "Status",
                            OldValue = firstfound.Application_Status,
                            NewValue = _request.Application_Status
                        };
                        var iteminsert6 = new ListByDiff
                        {
                            Field = "User Management",
                            OldValue = firstfound.User_Management_Integration,
                            NewValue = _request.User_Management_Integration
                        };
                        var iteminsert7 = new ListByDiff
                        {
                            Field = "Business Owner Name",
                            OldValue = firstfound.Business_Owner_Nama,
                            NewValue = _request.Business_Owner_Nama
                        };
                        var iteminsert8 = new ListByDiff
                        {
                            Field = "Business Owner Email",
                            OldValue = firstfound.Business_Owner_Email,
                            NewValue = _request.Business_Owner_Email
                        };
                        var iteminsert9 = new ListByDiff
                        {
                            Field = "Business Owner KBO",
                            OldValue = firstfound.Business_Owner_KBO,
                            NewValue = _request.Business_Owner_KBO
                        };
                        var iteminsert10 = new ListByDiff
                        {
                            Field = "Business Owner Jabatan",
                            OldValue = firstfound.Business_Owner_Jabatan,
                            NewValue = _request.Business_Owner_Jabatan
                        };
                        var iteminsert11 = new ListByDiff
                        {
                            Field = "Business Owner PIC",
                            OldValue = firstfound.Business_Owner_PIC,
                            NewValue = _request.Business_Owner_PIC
                        };
                        var iteminsert12 = new ListByDiff
                        {
                            Field = "Business Owner PIC Email",
                            OldValue = firstfound.Business_Owner_PIC_Email,
                            NewValue = _request.Business_Owner_PIC_Email
                        };
                        var iteminsert13 = new ListByDiff
                        {
                            Field = "Developer",
                            OldValue = firstfound.Developer,
                            NewValue = _request.Developer
                        };
                        var iteminsert14 = new ListByDiff
                        {
                            Field = "Business Analist",
                            OldValue = firstfound.Business_Analyst,
                            NewValue = _request.Business_Analyst
                        };
                        try
                        {
                            var convdate = Convert.ToDateTime(firstfound.Start_Development);
                            var iteminsert = new ListByDiff
                            {
                                Field = "Start Development",
                                OldValue = convdate.ToString("dd-MM-yyyy HH:mm:ss"),
                                NewValue = _request.Start_Development.ToString("dd-MM-yyyy HH:mm:ss")
                            };
                            _listDiff.Add(iteminsert);
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
                            var convdate = Convert.ToDateTime(firstfound.Start_Implementation);
                            var iteminsert = new ListByDiff
                            {
                                Field = "Start Implementation",
                                OldValue = convdate.ToString("dd-MM-yyyy HH:mm:ss"),
                                NewValue = _request.Start_Implementation.ToString("dd-MM-yyyy HH:mm:ss")
                            };
                            _listDiff.Add(iteminsert);
                        }
                        catch (Exception ex)
                        {
                            // Log and rethrow for any other unforeseen exceptions
                            var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                            //LogException(exceptionDetails);
                            //throw;  // Rethrow the exception to let the calling code handle it if needed
                        }

                        var iteminsert15 = new ListByDiff
                        {
                            Field = "Company Code",
                            OldValue = firstfound.Company_Code,
                            NewValue = _request.Company_Code
                        };
                        var iteminsert16 = new ListByDiff
                        {
                            Field = "Is Deleted",
                            OldValue = firstfound.IsDeleted,
                            NewValue = _request.IsDeleted
                        };
                        _listDiff.Add(iteminsert1);
                        _listDiff.Add(iteminsert2);
                        _listDiff.Add(iteminsert3);
                        _listDiff.Add(iteminsert4);
                        _listDiff.Add(iteminsert5);
                        _listDiff.Add(iteminsert6);
                        _listDiff.Add(iteminsert7);
                        _listDiff.Add(iteminsert8);
                        _listDiff.Add(iteminsert9);
                        _listDiff.Add(iteminsert10);
                        _listDiff.Add(iteminsert11);
                        _listDiff.Add(iteminsert12);
                        _listDiff.Add(iteminsert13);
                        _listDiff.Add(iteminsert14);
                        _listDiff.Add(iteminsert15);
                        _listDiff.Add(iteminsert16);
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

            _isLoading = false;
        }
    }
    private void OnValueSelecthandler(SelectEventArgs<GetSingleData> args)
    {
        args.E.ToString();
    }
    public async Task OnClickBtnApproved(EditContext formContext)
    {
        var formIsValid = formContext.Validate();
        if (formIsValid == false)
        {
            return;
        }

        _error = null;

        _isLoading = true;
        _request.IsApproved = "Approved";
        _request.Approved_Status = "Approved";
        _request.Reason = "";
        var response = await _dataService.UpdateDraftCatalogAsync(_request);

        if (response.Error is not null)
        {
            _isLoading = false;
            _strHeadDialog = "Proses Approved Draft Aplikasi Gagal";
            _strContentDialog = "";
            foreach (var item in response.Error.Details)
            {
                _strContentDialog += item + "<br />";
            }

            _strLinkDialog = "";
            Visibility = true;
            return;
        }

        _isLoading = false;
        _strHeadDialog = "Draft Aplikasi Berhasil di Approved";
        _strContentDialog = "Code Draft " + _request.Code_Apps + " telah di approved, code aplikasinya adalah " + response.Result.Code_App;
        _strLinkDialog = "ok";
        Visibility = true;

    }

    public async Task OnClickBtnReject(EditContext formContext)
    {
        var formIsValid = formContext.Validate();
        if (formIsValid == false)
        {
            return;
        }

        if (string.IsNullOrEmpty(_request.Reason))
        {
            _strHeadDialog = "Mohon untuk melengkapi Field Berikut";
            _strContentDialog = "Field Reason tidak boleh kosong";
            _strLinkDialog = "";
            Visibility = true;
            return;
        }

        _error = null;

        _isLoading = true;
        _request.IsApproved = "Rejected";
        _request.Approved_Status = "Rejected";
        var response = await _dataService.UpdateDraftCatalogAsync(_request);

        if (response.Error is not null)
        {
            _isLoading = false;
            _strHeadDialog = "Proses Rejected Draft Aplikasi Gagal";
            _strContentDialog = "";
            foreach (var item in response.Error.Details)
            {
                _strContentDialog += item + "<br />";
            }

            _strLinkDialog = "";
            Visibility = true;
            return;
        }

        _isLoading = false;
        _strHeadDialog = "Draft Aplikasi Di Tolak";
        _strContentDialog = "Code Draft " + _request.Code_Apps + " di tolak karena " + _request.Reason;
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
    }
    private void OnInvalidSubmit(EditContext editContext)
    {
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
