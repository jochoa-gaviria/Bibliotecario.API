using Bibliotecario.Common.Resources;
using Newtonsoft.Json;
using System.Net;


namespace Bibliotecario.Common.Models;

public class ResponseDto<T>
{
    #region properties
    [JsonProperty("codigo")]
    public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;

    [JsonProperty("mensaje")]
    public string Message { get; set; } = MessageResource.SuccessMessage;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T Data { get; set; }
    #endregion properties

    #region methods
    public void SetResponse(HttpStatusCode code, string message, object data = default)
    {
        Code = code;
        Message = message;
        if (data != null)
            Data = (T)data;

    }
    #endregion methods
}
