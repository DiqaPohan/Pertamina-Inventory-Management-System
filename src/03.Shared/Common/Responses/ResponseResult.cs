namespace Pertamina.SolutionTemplate.Shared.Common.Responses;

public class ResponseResult<T> where T : Response
{
    public T? Result { get; set; }
    public ErrorResponse? Error { get; set; }
}
