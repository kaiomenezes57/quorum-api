namespace Quorum.Application.Shared.Responses;

public class WebResponse<T>
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    public T? Data { get; }

    private WebResponse(bool isSuccess, string errorMessage, T data)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Data = data;
    }
    
    public static WebResponse<T?> Success(T? data) 
        => new(true, string.Empty, data);
    
    public static WebResponse<T?> Failure(string message) 
        => new(false, message, default);
}