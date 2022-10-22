using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Infrastructure.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            KeyValuePair<string, IEnumerable<string>>[] errors = context.ModelState
                .Where(c => c.Value.Errors.Any())
                .ToDictionary(c => c.Key, c => c.Value.Errors
                    .Select(c => c.ErrorMessage)).ToArray();
            context.Result = new BadRequestObjectResult(errors);
            return;
        }

        await next();
    }
}