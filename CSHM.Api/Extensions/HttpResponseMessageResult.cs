using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CSHM.Api.Extensions;

public class HttpResponseMessageResult : IActionResult
{
    private readonly HttpResponseMessage _responseMessage;

    public HttpResponseMessageResult(HttpResponseMessage responseMessage)
    {
        _responseMessage = responseMessage; // could add throw if null
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)_responseMessage.StatusCode;

        foreach (var header in _responseMessage.Content.Headers)
        {
            context.HttpContext.Response.Headers.TryAdd(header.Key, new StringValues(header.Value.ToArray()));
        }

        await using var stream = await _responseMessage.Content.ReadAsStreamAsync();
        await stream.CopyToAsync(context.HttpContext.Response.Body);
        await context.HttpContext.Response.Body.FlushAsync();
    }
}