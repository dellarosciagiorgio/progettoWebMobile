using Ganss.Xss;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

public class SanitizeInputFilter : IActionFilter
{
    private readonly HtmlSanitizer _sanitizer;

    public SanitizeInputFilter()
    {
        _sanitizer = new HtmlSanitizer();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var arg in context.ActionArguments.Values)
        {
            if (arg == null) continue;

            var props = arg.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                           .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);

            foreach (var prop in props)
            {
                var original = prop.GetValue(arg) as string;
                if (!string.IsNullOrEmpty(original))
                {
                    var sanitized = _sanitizer.Sanitize(original);
                    prop.SetValue(arg, sanitized);
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Non ci serve qui
    }
}