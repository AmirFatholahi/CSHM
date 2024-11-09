using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CSHM.Api.Extensions;

public class DecoderFallbackExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception.GetType() == typeof(DecoderFallbackException))
            context.Result = new BadRequestObjectResult("InvalidURL");
    }
}