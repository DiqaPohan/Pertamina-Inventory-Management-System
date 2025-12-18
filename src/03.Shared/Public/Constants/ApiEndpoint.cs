namespace Pertamina.SolutionTemplate.Shared.Public.Constants;
public static class ApiEndpoint
{
    public static class V1
    {
        public static class Public
        {
            public const string Segment = $"{nameof(V1)}/{nameof(Public)}";

            public static class RouteTemplateFor
            {
                public const string GetTableMasterData = nameof(GetTableMasterData);
                public const string GetApplicationTypes = nameof(GetApplicationTypes);
                public const string GetAllDataByKeyWithToken = nameof(GetAllDataByKeyWithToken);
                public const string GetSingleDataByKeyWithToken = nameof(GetSingleDataByKeyWithToken);
                public const string InsertDataWithToken = nameof(InsertDataWithToken);
                public const string UpdateDataByCodeAppsWithToken = nameof(UpdateDataByCodeAppsWithToken);
            }
        }
    }
}

