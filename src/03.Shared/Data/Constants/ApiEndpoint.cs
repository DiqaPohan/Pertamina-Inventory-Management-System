
namespace Pertamina.SolutionTemplate.Shared.Data.Constants;
public static class ApiEndpoint
{
    public static class V1
    {
        public static class Data
        {
            public const string Segment = $"{nameof(V1)}/{nameof(Data)}";

            public static class RouteTemplateFor
            {
                public const string DataId = "{appId:guid}";
                public const string AllData = nameof(AllData);
                public const string GetAllDataPaginated = nameof(GetAllDataPaginated);
                public const string InsertData = nameof(InsertData);
                public const string UpdateData = nameof(UpdateData);
                public const string DeleteData = nameof(DeleteData);
                public const string SingleDataByKey = "GetSingleDataByKey/{Key}&{KeyValue}";
                public const string GetAllDataByKeyWithToken = nameof(GetAllDataByKeyWithToken);
                public const string GetSingleDataByKeyWithToken = nameof(GetSingleDataByKeyWithToken);
                public const string AllDataByKeyWithToken = "GetAllDataByKeyWithToken/{Token}&{Key}&{KeyValue}&{ApplicationStatus}";
                public const string InsertDataWithToken = "InsertDataWithToken/{Token}&{AppName}&{AppArea}&{AppType}&{Description}&{Level_1}&{Level_2}&{Utilization}&{Status}&{BusinessOwnerPIC}&{BusinessOwner}&{Developer}&{Link}&{Users}&{StartDevelopment}&{StartImplementation}&{Criticality}&{HoldingArea}";
                public const string UpdateDataByIDWithToken = "UpdateDataByIDWithToken/{Token}&{AppID}&{AppName}&{AppArea}&{AppType}&{Description}&{Level_1}&{Level_2}&{Utilization}&{Status}&{BusinessOwnerPIC}&{BusinessOwner}&{Developer}&{Link}&{Users}&{StartDevelopment}&{StartImplementation}&{Criticality}&{HoldingArea}";
                public const string DeleteDataWithToken = "DeleteDataWithToken/{Token}&{Keys}";
                public const string SingleDataByKeyWithToken = "GetSingleDataByKeyWithToken/{Token}&{Key}&{KeyValue}&{ApplicationStatus}";
                public const string GetToken = nameof(GetToken);
                public const string GetTableMasterData = nameof(GetTableMasterData);
                public const string GetTableRequestData = nameof(GetTableRequestData);
                public const string GetTableRequestDataPaginated = nameof(GetTableRequestDataPaginated);
                //public const string GetTableHistoricalApplicationPhase = nameof(GetTableHistoricalApplicationPhase);
                //public const string GetTableHistoricalApplicationPhasePaginated = nameof(GetTableHistoricalApplicationPhasePaginated);
                //public const string GetTableDraftHistoricalApplicationPhase = nameof(GetTableDraftHistoricalApplicationPhase);
                //public const string GetTableDraftHistoricalApplicationPhasePaginated = nameof(GetTableDraftHistoricalApplicationPhasePaginated);
                public const string AddDraftCatalog = nameof(AddDraftCatalog);
                public const string UpdateDraftCatalog = nameof(UpdateDraftCatalog);
                //public const string AddDraftHistoricalApplicationPhase = nameof(AddDraftHistoricalApplicationPhase);
                //public const string UpdateDraftHistoricalApplicationPhase = nameof(UpdateDraftHistoricalApplicationPhase);
            }
        }
    }
}

