namespace Quorum.Application.Shared.Responses;

public class DefaultResponse<T>
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    public T? Data { get; }

    private DefaultResponse(bool isSuccess, string errorMessage, T data)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Data = data;
    }
    
    public static DefaultResponse<T?> Success(T? data) 
        => new(true, string.Empty, data);
    
    public static DefaultResponse<T?> Failure(string message) 
        => new(false, message, default);
}