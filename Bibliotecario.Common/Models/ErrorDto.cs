using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Bibliotecario.Common.Models;

public class ErrorDto
{
    #region properties
    [JsonPropertyName("codigo")]
    public string Code { get; set; }

    [JsonPropertyName("mensaje")]
    public string Message { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, List<string>> Errors { get; set; }
    #endregion properties

    #region methods
    public ErrorDto(string? code = default, string? message = default)
    {
        Code = code;
        Message = message;
    }
    #endregion methods
}
