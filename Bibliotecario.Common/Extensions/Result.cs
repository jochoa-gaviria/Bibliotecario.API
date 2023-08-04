using Bibliotecario.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bibliotecario.Common.Extensions;

public static class Result
{
    public static ObjectResult Execute<T>(ResponseDto<T> modelResponse)
    {
        ObjectResult result;
        int httpStatusCode = modelResponse.Code.GetHashCode();

        if (modelResponse != null && modelResponse.Data != null)
            result = new ObjectResult(modelResponse.Data) { StatusCode = httpStatusCode };
        else if (!string.IsNullOrEmpty(modelResponse?.Message))
            result = new ObjectResult(new ErrorDto(httpStatusCode.ToString(), modelResponse.Message)) { StatusCode = httpStatusCode };
        else
            result = new ObjectResult(default) { StatusCode = httpStatusCode };

        return result;
    }
}