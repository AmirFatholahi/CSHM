using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Extensions;

public class ForbiddenPage : ForbidResult
{
    public override async Task ExecuteResultAsync(ActionContext context)
    {
        string html = File.ReadAllText(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\wwwroot\\html\\forbidenPage.html");

        var contentResult = new ContentResult
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Content = html,
            ContentType = "text/html; charset=utf-8"
        };

        await contentResult.ExecuteResultAsync(context);

    }
}