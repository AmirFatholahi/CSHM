using Microsoft.AspNetCore.Mvc;

namespace CSHM.Api.Extensions;

public class CustomBadRequest : ValidationProblemDetails
{
    public CustomBadRequest(ActionContext context) : base(context.ModelState)
    {
#if DEBUG
        var a = 1;
#else
            
            Instance = "Custom Bad request For Security";
            Status = 400;
            Title = "Your model is not valid";
            Type = "Bad Request";
            
            var errors = Errors.ToList();
            foreach (var errorValue in Errors)
            {
                Errors.Remove(errorValue);
            }

            foreach (var error in errors)
            {
                Errors.Add(new KeyValuePair<string, string[]>(error.Key,new []{""}));
            }

#endif
    }
}