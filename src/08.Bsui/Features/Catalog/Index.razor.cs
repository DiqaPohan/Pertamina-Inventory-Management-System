using System.Globalization;
using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Common.Constants;
using Pertamina.SolutionTemplate.Bsui.Features.Catalog.Components;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Extensions;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Pertamina.SolutionTemplate.Shared.Data.Commands.DetailData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;
using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleDataHistoricalApplicationPhase;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Charts;
using Syncfusion.Blazor.Grids;
namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog;

public partial class Index
{
    private bool _isLoading;
    private ErrorResponse? _error;
    private readonly List<BreadcrumbItem> _breadcrumbItems = new();
    private string _greetings = default!;
    private int _totalapps = default!;
    private int _totalappskpipusat = default!;
    private int _totalappskpiru = default!;
    private int _totalappsholding = default!;
    private List<GetSingleData> _dataCount = default!;
    private List<GetSingleData> _dataCountAll = default!;
    private List<GetSingleData> _dataCountKPI = default!;
    private List<GetSingleData> _dataCountKPIRU = default!;
    private List<GetSingleData> _dataCountHolding = default!;
    private List<GetSingleData> _dataCountTemporary = default!;
    private List<GetSingleDataHistoricalApplicationPhase> _dataHistoricalCount = default!;
    private List<GetSingleData> _dataCountAktif = default!;
    private List<GetSingleData> _dataCountNonAktif = default!;
    //private List<GetSingleData> _dataCountUknown = default!;
    //private List<GroupByType> _dataByTypesCount = default!;
    private List<ListByType> _listByTypes = default!;
    private DateTimeOffset? StartDate { get; set; } = DateTimeOffset.Now;
    private DateTimeOffset? EndDate { get; set; } = DateTimeOffset.Now;
    public class GroupByType
    {
        public string Text { get; set; }
        public long Number { get; set; }
        public List<GetSingleData> Data { get; set; }
    }
    public class ListByType
    {
        public string Text { get; set; }
        public long Number { get; set; }
        public List<GetSingleData> Data { get; set; }
    }
    public class ListByPhase
    {
        public string CodeApp { get; set; }
        public string NameApp { get; set; }
        public string AreaApp { get; set; }
        public string PhaseApp { get; set; }
        public DateTimeOffset Startdate { get; set; }
        public DateTimeOffset Enddate { get; set; }
        public List<ListByPhaseDate> Ldate { get; set; }
    }
    public string InnerRadius { get; set; } = "0%";
    public string Radius { get; set; } = "70%";
    public string FontColor { get; set; } = "white";
    public bool Rotation { get; set; }
    public AccumulationLabelPosition LabelPosition { get; set; } = AccumulationLabelPosition.Inside;
    public TextWrap Wrap { get; set; } = TextWrap.Normal;
    public string ConnectorLength { get; set; } = "20px";
    public string Size { get; set; } = "12px";
    public class PieData
    {
        public string XValue { get; set; }
        public double YValue { get; set; }
        public string Palettes { get; set; }
        public string DataLabelMappingName { get; set; }
    }

    public string Width { get; set; } = "90%";
    public bool Rotate { get; set; } = false;

    public List<PieData> PieChartPoints { get; set; } = new List<PieData>
    {
        new () { XValue = "Aktif", YValue = 3 , DataLabelMappingName = "status : total aplikasi 3"},
        new () { XValue = "Tidak Aktif", YValue = 3 , DataLabelMappingName = "status : total aplikasi 3"},
        new () { XValue = "Tidak Diketahui",  YValue = 3  , DataLabelMappingName = "status : total aplikasi 3"}
    };
    public List<PieData> PieChartPointsHead { get; set; } = new List<PieData>();
    public List<PieData> PieChartPointsAppType { get; set; } = new List<PieData>();
    public List<PieData> PieChartPointsHeadAppType { get; set; } = new List<PieData>();
    public class StackedColumnChartData
    {
        public string Year { get; set; }
        public double Active { get; set; }
        public double Rationalize { get; set; }
        public double NonActive { get; set; }
        public double Total { get; set; }
        public string Text { get; set; }
        public string ChartType { get; set; }
        public List<ListByPhase> Data { get; set; }
    }
    public List<StackedColumnChartData> ChartStackedPoints { get; set; } = new List<StackedColumnChartData>
    {
        new () { Year = "Oktober", Active = 2, NonActive = 1, Total = 3 },
        new () { Year = "November", Active = 3, NonActive = 2, Total = 5 },
        new () { Year = "Desember", Active = 3, NonActive = 2, Total = 5 },
    };
    public string CSS_Cursor { get; set; } = "cursor: pointer !important";
    private bool Visibility { get; set; } = true;
    private bool Visibilitygridnonhistrocial { get; set; } = true;
    private bool Visibilitygridhistrocial { get; set; } = true;
    private bool ShowButton { get; set; } = false;
    private string? _strHeadDialog;
    private string? _strLinkDialog;
    private SfGrid<GetSingleData>? _blazorDataGridNonHistorical;
    //private SfGrid<ListByPhase>? _blazorDataGridHistorical;
    //private List<ListByPhase> _lphaseshead = new();
    private List<ListByPhase> _lphaseschild = new();
    public string[] _palettes = new string[] { "green", "red", "gray" };
    private void DialogOpen(object args)
    {
        ShowButton = false;
    }
    private void DialogClose(object args)
    {
        ShowButton = true;
    }
    private void SetupBreadcrumb()
    {
        _breadcrumbItems.Add(CommonBreadcrumbFor.Catalog);
        _breadcrumbItems.Add(CommonBreadcrumbFor.Active(CommonDisplayTextFor.Overview));
    }
    protected override async Task OnInitializedAsync()
    {
        SetupBreadcrumb();
        _greetings = $"Good {DateTimeOffset.Now.ToFriendlyTimeDisplayText()}";
        Size = "12px";
        Rotation = false;
        ConnectorLength = "5%";
        Width = "100%";
        Rotate = true;
        Visibility = false;
        await GetDatas();
    }
    public class ListByPhaseDate
    {
        public int Intdate { get; set; }
        public DateTime Date { get; set; }
    }

    private List<ListByPhaseDate> GetTotalMonthsFrom(DateTimeOffset dt1, DateTimeOffset dt2)
    {
        var ltemptemp = new List<ListByPhaseDate>();
        var earlyDate = (dt1 > dt2) ? dt2.Date : dt1.Date;
        var lateDate = (dt1 > dt2) ? dt1.Date : dt2.Date;

        var diff = Enumerable.Range(0, int.MaxValue)
                     .Select(e => earlyDate.AddMonths(e))
                     .TakeWhile(e => e <= lateDate)
                     .Select(e => e);

        var monthsDiff = 1;
        foreach (var date in diff)
        {
            var iteminsert = new ListByPhaseDate
            {
                Intdate = monthsDiff,
                Date = date
            };
            ltemptemp.Add(iteminsert);
            monthsDiff++;
        }
        ////// Start with 1 month's difference and keep incrementing
        ////// until we overshoot the late date

        ////while (earlyDate.AddMonths(monthsDiff) <= lateDate)
        ////{
        ////    var dateconv = Convert.ToDateTime(earlyDate.ToString("yyyy-MM-dd HH:mm:ss"));
        ////    dateconv.AddMonths(monthsDiff);
        ////}

        return ltemptemp;
    }
    private async Task GetDatas()
    {
        _dataCount = new List<GetSingleData>();
        _dataHistoricalCount = new List<GetSingleDataHistoricalApplicationPhase>();
        _listByTypes = new List<ListByType>();
        _dataCountAktif = new List<GetSingleData>();
        _dataCountNonAktif = new List<GetSingleData>();
        //_dataCountUknown = new List<GetSingleData>();
        _dataCountKPI = new List<GetSingleData>();
        _dataCountKPIRU = new List<GetSingleData>();
        _dataCountHolding = new List<GetSingleData>();
        _dataCountTemporary = new List<GetSingleData>();
        _isLoading = true;
        var response = await _dataService.GetAllDataAsync();
        //var responsehistorical = await _dataService.GetAllHistoricalApplicationPhaseDataAsync();
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
                _dataCountAll = response.Result!.Items.ToList();
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

                var tempdata = _dataCountAll.Where(pp => pp.IsDeleted == "false").ToList();
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
                PieChartPoints = new List<PieData>();
                ChartStackedPoints = new List<StackedColumnChartData>();
                var iaktif = 0;
                var inonaktif = 0;
                var iunknown = 0;
                _totalapps = _dataCount.Count;
                var groupbyarea = _dataCount.GroupBy(pp => pp.Application_Area).ToList();
                foreach (var group in groupbyarea)
                {
                    if (string.IsNullOrEmpty(group.Key))
                    {

                    }
                    else
                    {
                        if (group.Key.ToLower().Contains("persero"))
                        {
                            _totalappsholding += group.Count();
                            _dataCountHolding.AddRange(group);
                        }
                        else if (group.Key.ToLower().Contains("head office"))
                        {
                            _totalappskpipusat += group.Count();
                            _dataCountKPI.AddRange(group);
                        }
                        else if (group.Key.ToLower().Contains("ru "))
                        {
                            _totalappskpiru += group.Count();
                            _dataCountKPIRU.AddRange(group);
                        }
                    }
                }

                var groupbystatus = _dataCount.GroupBy(pp => pp.Application_Status).ToList();
                foreach (var group in groupbystatus)
                {
                    if (string.IsNullOrEmpty(group.Key))
                    {

                    }
                    else
                    {
                        if (group.Key == "Aktif")
                        {
                            iaktif = group.Count();
                            _dataCountAktif.AddRange(group);
                        }
                        else if (group.Key == "Tidak Aktif")
                        {
                            inonaktif = group.Count();
                            _dataCountNonAktif.AddRange(group);
                        }
                        //else
                        //{
                        //    iunknown = group.Count();
                        //    _dataCountUknown.AddRange(group);
                        //}

                    }
                }

                PieChartPoints.Add(new PieData()
                {
                    XValue = "Aktif",
                    YValue = iaktif,
                    DataLabelMappingName = "Aktif : " + iaktif,
                    Palettes = "green"

                });

                PieChartPoints.Add(new PieData()
                {
                    XValue = "Tidak Aktif",
                    YValue = inonaktif,
                    DataLabelMappingName = "Tidak Aktif : " + inonaktif,
                    Palettes = "red"
                });

                //PieChartPoints.Add(new PieData()
                //{
                //    XValue = "Tidak diketahui",
                //    YValue = iunknown,
                //    DataLabelMappingName = "Tidak diketahui : " + iunknown,
                //    Palettes = "gray"
                //});
                PieChartPointsHead = PieChartPoints;

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
            //    var tempdata = responsehistorical.Result!.Items.ToList();
            //    var ltempphases = new List<ListByPhase>();
            //    var lmonth = new List<DateTime>
            //{
            //    System.DateTime.Now.AddMonths(-6),
            //    System.DateTime.Now.AddMonths(-5),
            //    System.DateTime.Now.AddMonths(-4),
            //    System.DateTime.Now.AddMonths(-3),
            //    System.DateTime.Now.AddMonths(-2),
            //    System.DateTime.Now.AddMonths(-1),
            //    System.DateTime.Now,
            //};

            //    ChartStackedPoints = new List<StackedColumnChartData>();
            //    foreach (var i in lmonth)
            //    {
            //        var dt = new StackedColumnChartData
            //        {
            //            Year = i.ToString("MMM"),
            //            Active = 0,
            //            Rationalize = 0,
            //            NonActive = 0,
            //            Total = 0
            //        };
            //        ChartStackedPoints.Add(dt);
            //    }

            //    _lphaseshead = new List<ListByPhase>();
            //    try
            //    {
            //        var minus6month = System.DateTime.Now.AddMonths(-6);
            //        var getonlyminus6month = tempdata.Where(pp =>
            //        pp.Year >= minus6month.Year
            //        ).ToList();
            //        var groupbyid = getonlyminus6month.GroupBy(pp => pp.Code_Apps).ToList();
            //        foreach (var item in groupbyid)
            //        {
            //            var strname = "";
            //            var strarea = "";
            //            try
            //            {
            //                var foundata = _dataCountAll.Where(pp => pp.Code_Apps == item.Key).SingleOrDefault();
            //                strname = foundata.Application_Name;
            //                strarea = foundata.Application_Area;
            //            }
            //            catch
            //            {

            //            }

            //            var foundbyid = tempdata.Where(pp => pp.Code_Apps == item.Key).ToList();
            //            var orderbydate = foundbyid.OrderBy(pp => pp.Date).ToList();
            //            var groupbyphase = orderbydate.GroupBy(pp => pp.Phase).ToList();
            //            foreach (var subitem in groupbyphase)
            //            {
            //                foreach (var subsubitem in subitem)
            //                {

            //                    try
            //                    {
            //                        var insertphase = new ListByPhase
            //                        {
            //                            CodeApp = item.Key,
            //                            NameApp = strname,
            //                            PhaseApp = subitem.Key,
            //                            AreaApp = strarea,
            //                            Startdate = (DateTimeOffset)subsubitem.Date
            //                        };
            //                        if (subitem.Key == "Tidak Aktif")
            //                        {
            //                            insertphase.Enddate = (DateTimeOffset)subsubitem.Date;
            //                        }

            //                        ltempphases.Add(insertphase);
            //                    }
            //                    catch
            //                    {

            //                    }

            //                }

            //            }
            //        }
            //    }
            //    catch
            //    {

            //    }

            //    try
            //    {
            //        var ltempphasesv1 = new List<ListByPhase>();

            //        try
            //        {
            //            var groupbyid = ltempphases.GroupBy(pp => pp.CodeApp).ToList();
            //            foreach (var item in groupbyid)
            //            {
            //                var orderbydate = item.OrderBy(pp => pp.Startdate).ToList();
            //                var groupbyphase = orderbydate.GroupBy(pp => pp.PhaseApp).ToList();
            //                var igrp = 0;
            //                foreach (var subitem in groupbyphase)
            //                {
            //                    if (subitem.Key != "Tidak Aktif")
            //                    {
            //                        foreach (var subsubitem in subitem)
            //                        {
            //                            var dtoffset = (DateTimeOffset)DateTime.Today;
            //                            var bconv = false;
            //                            try
            //                            {
            //                                dtoffset = groupbyphase[igrp + 1].FirstOrDefault().Startdate;
            //                                bconv = true;
            //                            }
            //                            catch
            //                            {

            //                            }

            //                            if (bconv)
            //                            {
            //                                subsubitem.Enddate = dtoffset;
            //                                ltempphasesv1.Add(subsubitem);
            //                            }
            //                            else
            //                            {
            //                                subsubitem.Enddate = subsubitem.Startdate;
            //                                ltempphasesv1.Add(subsubitem);
            //                            }

            //                        }
            //                    }
            //                    else
            //                    {
            //                        ltempphasesv1.AddRange(subitem.ToList());
            //                    }

            //                    igrp += 1;
            //                }
            //            }

            //        }
            //        catch
            //        {

            //        }

            //        try
            //        {
            //            var groupbyid = ltempphasesv1.GroupBy(pp => pp.CodeApp).ToList();
            //            foreach (var item in groupbyid)
            //            {
            //                var orderbydate = item.OrderBy(pp => pp.Startdate).ToList();
            //                foreach (var subitem in orderbydate)
            //                {
            //                    try
            //                    {

            //                        var calcmonth = GetTotalMonthsFrom(subitem.Startdate, subitem.Enddate);
            //                        var dumpinsert = subitem;
            //                        dumpinsert.Ldate = new List<ListByPhaseDate>();
            //                        dumpinsert.Ldate.AddRange(calcmonth);
            //                        _lphaseshead.Add(dumpinsert);
            //                    }
            //                    catch
            //                    {

            //                    }
            //                }
            //            }

            //        }
            //        catch
            //        {

            //        }
            //    }
            //    catch
            //    {

            //    }

            //    _dataHistoricalCount.AddRange(tempdata);

            //    var ltempdata = new List<StackedColumnChartData>();
            //    var groupbycodeapp = _lphaseshead.GroupBy(pp => pp.CodeApp).ToList();
            //    foreach (var groupcode in groupbycodeapp)
            //    {
            //        try
            //        {
            //            var groupbystartdate = groupcode.GroupBy(pp => pp.Startdate).ToList();
            //            foreach (var groupstart in groupbystartdate)
            //            {
            //                foreach (var substart in groupstart)
            //                {
            //                    foreach (var date in lmonth)
            //                    {
            //                        var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            //                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            //                        var foundate = substart.Ldate.Where(
            //                            pp =>
            //                            pp.Date >= firstDayOfMonth &&
            //                            pp.Date <= lastDayOfMonth

            //                        ).ToList();
            //                        if (foundate.Any())
            //                        {
            //                            if (string.IsNullOrEmpty(substart.PhaseApp))
            //                            {
            //                                var dt = new StackedColumnChartData
            //                                {
            //                                    Year = firstDayOfMonth.ToString("MMM"),
            //                                    Active = 0,
            //                                    Rationalize = 0,
            //                                    NonActive = 1,
            //                                    Total = 1,
            //                                    Data = new List<ListByPhase>()
            //                                };
            //                                dt.Data.Add(substart);
            //                                ltempdata.Add(dt);
            //                            }
            //                            else
            //                            {
            //                                if (substart.PhaseApp == "Aktif")
            //                                {
            //                                    var dt = new StackedColumnChartData
            //                                    {
            //                                        Year = firstDayOfMonth.ToString("MMM"),
            //                                        Active = 1,
            //                                        NonActive = 0,
            //                                        Rationalize = 0,
            //                                        Total = 1,
            //                                        Data = new List<ListByPhase>()
            //                                    };
            //                                    dt.Data.Add(substart);
            //                                    ltempdata.Add(dt);
            //                                }
            //                                else if (substart.PhaseApp == "Tidak Aktif")
            //                                {
            //                                    var dt = new StackedColumnChartData
            //                                    {
            //                                        Year = firstDayOfMonth.ToString("MMM"),
            //                                        Active = 0,
            //                                        NonActive = 1,
            //                                        Rationalize = 0,
            //                                        Total = 1,
            //                                        Data = new List<ListByPhase>()
            //                                    };
            //                                    dt.Data.Add(substart);
            //                                    ltempdata.Add(dt);
            //                                }
            //                                else
            //                                {
            //                                    var dt = new StackedColumnChartData
            //                                    {
            //                                        Year = firstDayOfMonth.ToString("MMM"),
            //                                        Active = 0,
            //                                        NonActive = 0,
            //                                        Rationalize = 1,
            //                                        Total = 1,
            //                                        Data = new List<ListByPhase>()
            //                                    };
            //                                    dt.Data.Add(substart);
            //                                    ltempdata.Add(dt);
            //                                }
            //                            }
            //                        }
            //                    }

            //                }
            //            }
            //        }
            //        catch
            //        {

            //        }
            //    }

            //    foreach (var item in ChartStackedPoints)
            //    {
            //        try
            //        {
            //            var iaktif = 0;
            //            var inonaktif = 0;
            //            var itotal = 0;
            //            var iRationalize = 0;
            //            item.Data = new List<ListByPhase>();
            //            try
            //            {
            //                var groupbytemp = ltempdata.Where(pp => pp.Year == item.Year).ToList();
            //                foreach (var groupvalue in groupbytemp)
            //                {
            //                    if (groupvalue.Active != 0)
            //                    {
            //                        iaktif++;
            //                    }

            //                    if (groupvalue.NonActive != 0)
            //                    {
            //                        inonaktif++;
            //                    }

            //                    if (groupvalue.Total != 0)
            //                    {
            //                        itotal++;
            //                    }

            //                    if (groupvalue.Rationalize != 0)
            //                    {
            //                        iRationalize++;
            //                    }

            //                    item.Data.AddRange(groupvalue.Data);
            //                }

            //            }
            //            catch
            //            {

            //            }

            //            item.Active = iaktif;
            //            item.NonActive = inonaktif;
            //            item.Rationalize = iRationalize;
            //            item.Total = itotal;
            //            item.Text = itotal.ToString();
            //            var maxint = 0;
            //            var sket = "";
            //            if (iaktif > maxint)
            //            {
            //                maxint = iaktif;
            //                sket = "active";
            //            }

            //            if (inonaktif > maxint)
            //            {
            //                maxint = inonaktif;
            //                sket = "non active";
            //            }

            //            if (iRationalize > maxint)
            //            {
            //                maxint = iRationalize;
            //                sket = "rationalize";
            //            }

            //            item.ChartType = sket;
            //        }
            //        catch
            //        {

            //        }

            //    }

            //}
            //catch
            //{

            //}

            //try
            //{

            //    _dataByTypesCount = _dataCount.GroupBy(i => i.Application_Type)
            //       .Select(grp => new GroupByType
            //       {
            //           Text = grp.Key,
            //           Number = grp.Count(),
            //           Data = grp.ToList()
            //       })
            //       .ToList();
            //    foreach (var group in _dataByTypesCount)
            //    {
            //        PieChartPointsAppType.Add(new PieData()
            //        {
            //            XValue = group.Text,
            //            YValue = double.Parse(group.Number.ToString()),
            //            Palettes = "green"

            //        });
            //        _listByTypes.Add(new ListByType { Text = group.Text, Number = long.Parse(group.Number.ToString()), Data = group.Data });
            //    }

            //    PieChartPointsHeadAppType = PieChartPointsAppType;
            //}
            //catch
            //{

            //}

            _isLoading = false;
        }
    }
    private void AccPointClick_Status(AccumulationPointEventArgs args)
    {
        switch (args.Point.X)
        {
            case "Aktif":
                try
                {
                    _strHeadDialog = "Detail Aplikasi Berdasarkan Status " + args.Point.X;
                    _dataCountTemporary = _dataCountAktif;
                    _strLinkDialog = "";
                    Visibilitygridnonhistrocial = false;
                    Visibilitygridhistrocial = true;
                    Visibility = true;
                }
                catch (Exception ex)
                {
                    // Log and rethrow for any other unforeseen exceptions
                    var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                    //LogException(exceptionDetails);
                    //throw;  // Rethrow the exception to let the calling code handle it if needed
                }

                break;
            //case "Tidak diketahui":
            //    try
            //    {
            //        _strHeadDialog = "Detail Aplikasi Berdasarkan Status " + args.Point.Label;
            //        _dataCountTemporary = _dataCountUknown;
            //        _strLinkDialog = "";
            //        Visibilitygridnonhistrocial = false;
            //        Visibilitygridhistrocial = true;
            //        Visibility = true;
            //    }
            //    catch
            //    {

            //    }

            //    break;
            case "Tidak Aktif":
                try
                {
                    _strHeadDialog = "Detail Aplikasi Berdasarkan Status " + args.Point.X;
                    _dataCountTemporary = _dataCountNonAktif;
                    _strLinkDialog = "";
                    Visibilitygridnonhistrocial = false;
                    Visibilitygridhistrocial = true;
                    Visibility = true;
                }
                catch (Exception ex)
                {
                    // Log and rethrow for any other unforeseen exceptions
                    var exceptionDetails = $"Exception Type: {ex.GetType()}, Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                    //LogException(exceptionDetails);
                    //throw;  // Rethrow the exception to let the calling code handle it if needed
                }

                break;
        }

        StateHasChanged();
    }
    private void AccPointClick_Phase(PointEventArgs args)
    {
        switch (args.Point.X)
        {
            case null:
                break;
            case "":
                break;
            default:
                _strHeadDialog = "Detail Aplikasi Berdasarkan Phase Selama Bulan " + args.Point.X;
                var data = ChartStackedPoints.Where(pp => pp.Year == args.Point.X).ToList();
                _lphaseschild = data.FirstOrDefault().Data;
                _strLinkDialog = "";
                Visibilitygridnonhistrocial = true;
                Visibilitygridhistrocial = false;
                Visibility = true;
                break;
        }

        StateHasChanged();
    }

    private void AxisLabel(AxisLabelRenderEventArgs args)
    {
        if (args.Value is > 999999 or < (-999999))
        {
            args.Text = args.Value.ToString("0,,.##M", CultureInfo.InvariantCulture);
        }
    }
    private async Task ShowDialog(string strapp, string level1)
    {
        var request = new DetailDataRequest();

        try
        {
            var foundata = _dataCount.Where(pp => pp.Application_Name == strapp && pp.Capability_Level_1 == level1).ToList();
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
            throw;  // Rethrow the exception to let the calling code handle it if needed
        }

        var parameters = new DialogParameters
        {
            { nameof(DialogDetail.Request), request }
        };

        var dialog = _dialogService.Show<DialogDetail>(strapp, parameters);
        var result = await dialog.Result;

    }
    private async Task OnShowDialogDetails(string stype)
    {
        if (stype == "totalaps")
        {
            _strHeadDialog = "Detail Seluruh Aplikasi";
            _dataCountTemporary = _dataCount;
            _strLinkDialog = "";
            Visibilitygridnonhistrocial = false;
            Visibilitygridhistrocial = true;
            Visibility = true;

        }
        else if (stype == "totalapskpi")
        {
            _strHeadDialog = "Detail Aplikasi Yang Berada Di KPI Pusat";
            _dataCountTemporary = _dataCountKPI;
            _strLinkDialog = "";
            Visibilitygridnonhistrocial = false;
            Visibilitygridhistrocial = true;
            Visibility = true;
        }
        else if (stype == "totalapru")
        {
            _strHeadDialog = "Detail Aplikasi Yang Berada Di KPI Refinery Unit";
            _dataCountTemporary = _dataCountKPIRU;
            _strLinkDialog = "";
            Visibilitygridnonhistrocial = false;
            Visibilitygridhistrocial = true;
            Visibility = true;
        }
        else if (stype == "totalapholding")
        {
            _strHeadDialog = "Detail Aplikasi Yang Berada Di Holding";
            _dataCountTemporary = _dataCountHolding;
            _strLinkDialog = "";
            Visibilitygridnonhistrocial = false;
            Visibilitygridhistrocial = true;
            Visibility = true;
        }
        else
        {
            var types = _listByTypes.Where(pp => pp.Text == stype).SingleOrDefault();
            if (types.Data.Count != 0)
            {
                _strHeadDialog = "Detail Aplikasi Berdasarkan Tipe " + stype;
                _dataCountTemporary = types.Data;
                _strLinkDialog = "";
                Visibilitygridnonhistrocial = false;
                Visibilitygridhistrocial = true;
                Visibility = true;
            }
        }
    }
}
