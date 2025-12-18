using MudBlazor;
using Pertamina.SolutionTemplate.Shared.Common.Responses;

namespace Pertamina.SolutionTemplate.Bsui.Common.Extensions;

public static class PaginatedListResponseExtensions
{
    public static TableData<T> ToTableData<T>(this PaginatedListResponse<T> result)
    {
        return new TableData<T>() { TotalItems = result.TotalCount, Items = result.Items };
    }
}
