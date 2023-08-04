using Bibliotecario.Common.Models;
using Bibliotecario.Common.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Net;


namespace Bibliotecario.Common.Extensions;

public static class ModelState
{
    public static void AddModelState(this IServiceCollection services)
    {
        services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context => SetModelState(context.ModelState);
        });
    }

    private static ObjectResult SetModelState(ModelStateDictionary modelState)
    {
        ObjectResult result = default;
        ErrorDto errorDto = new ErrorDto(HttpStatusCode.BadRequest.GetHashCode().ToString(), MessageResource.BadRequestMessage)
        {
            Errors = new Dictionary<string, List<string>>()
        };

        foreach (var item in modelState)
        {
            IEnumerable<string> errorMessages = item.Value.Errors.Select(error => error.ErrorMessage);
            List<string> validErrorMessages = new List<string>();

            validErrorMessages.AddRange(errorMessages.Where(message => !string.IsNullOrEmpty(message)));

            if (validErrorMessages.Any())
            {
                if (errorDto.Errors.ContainsKey(item.Key))
                    errorDto.Errors[item.Key].AddRange(validErrorMessages);
                else
                    errorDto.Errors.Add(item.Key, validErrorMessages.ToList());
            }
        }

        if (errorDto.Errors.Any())
            result = new ObjectResult(errorDto) { StatusCode = (int)HttpStatusCode.BadRequest };

        return result;
    }
}