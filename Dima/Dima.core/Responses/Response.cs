using System.Text.Json.Serialization;

namespace Dima.core.Responses;

public class Response<TData>
{
    public readonly int _code;

    // Parameterless Constructor 
    [JsonConstructor]
    public Response() => 
        _code = Configuration.DefaultStatusCode;
    // Expression Body
    
    public Response(
        TData data, 
        int code = Configuration.DefaultStatusCode, 
        string? message = null)
    {
        _code = code;
        Data = data;
        Message = message;
    }
    public TData? Data { get; set; }
    public string? Message { get; set; } = string.Empty;

    [JsonIgnore]
    public bool isSuccess 
        => _code is >= 200 and <= 299;
}