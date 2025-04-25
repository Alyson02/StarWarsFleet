namespace StarWarsFleet.Domain.Models;

public class ResponseModel<T>
{
    public T? Data { get; }
    public bool Success { get; }
    public string Message { get; }
    public List<string> Errors { get; }
    public DateTime Timestamp { get; }

    protected ResponseModel(bool success, string message, T? data, List<string>? errors = null)
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors ?? new List<string>();
        Timestamp = DateTime.UtcNow;
    }

    public static ResponseModel<T> CreateSuccess(T data, string message = "Operação realizada com sucesso")
        => new(true, message, data);

    public static ResponseModel<T> CreateError(string message, List<string>? errors = null)
        => new(false, message, default, errors);

    public static ResponseModel<T> FromException(Exception ex)
        => new(false, "Ocorreu um erro ao processar a solicitação", default, 
            new List<string> { ex.Message });
}

public class ResponseModel : ResponseModel<object>
{
    private ResponseModel(bool success, string message, object? data, List<string>? errors = null) 
        : base(success, message, data, errors)
    {
    }

    public static ResponseModel CreateSuccess(string message = "Operação realizada com sucesso")
        => new(true, message, null);

    public new static ResponseModel CreateError(string message, List<string>? errors = null)
        => new(false, message, null, errors);

    public new static ResponseModel FromException(Exception ex)
        => new(false, "Ocorreu um erro ao processar a solicitação", null,
            [ex.Message]);
}