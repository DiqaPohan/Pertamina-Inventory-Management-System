using Microsoft.AspNetCore.Mvc;
using Pertamina.SolutionTemplate.Shared.Common.Responses;

namespace Pertamina.SolutionTemplate.WebApi.Common.Extensions;

public static class FileResponseExtension
{
    public static FileContentResult AsFile(this FileResponse fileResponse)
    {
        return new FileContentResult(fileResponse.Content, fileResponse.ContentType)
        {
            FileDownloadName = fileResponse.FileName
        };
    }
}
