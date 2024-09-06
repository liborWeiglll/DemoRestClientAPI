namespace Demo.Contracts;

public class ApiResponse<T>
{
    public ApiResponse(T response)
    {
        Data = response;
        IsSuccess = true;
    }

    public ApiResponse(string error)
    {
        Error = error;
        IsSuccess = false;
    }

    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Error { get; set; }
}