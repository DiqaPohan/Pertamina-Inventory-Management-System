using Pertamina.SolutionTemplate.Application.Services.Ecm.Models.UploadContent;

namespace Pertamina.SolutionTemplate.Application.Services.Ecm;

public interface IEcmService
{
    Task<UploadContentResponse> UploadContentAsync(UploadContentRequest request, CancellationToken cancellationToken);
}
