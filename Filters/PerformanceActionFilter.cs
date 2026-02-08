using Microsoft.AspNetCore.Mvc.Filters;

namespace capstone_policy_management.Filters;


public class PerformanceActionFilter : IActionFilter
{
    private readonly ILogger<PerformanceActionFilter> _logger;

    public PerformanceActionFilter(ILogger<PerformanceActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var requestTime = DateTime.UtcNow;
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];
        
        Console.WriteLine($"Request Time: {requestTime:yyyy-MM-dd HH:mm:ss.fff} for {controller}/{action}");
        
        context.HttpContext.Items["RequestTime"] = requestTime;
    }

   
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var responseTime = DateTime.UtcNow;
        var requestTime = context.HttpContext.Items["RequestTime"] as DateTime?;
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];
        
    
        if (requestTime.HasValue)
        {
            var durationMs = (responseTime - requestTime.Value).TotalMilliseconds;
            var durationSeconds = durationMs / 1000;
            
            Console.WriteLine($"Response Time: {durationMs:F2}ms ({durationSeconds:F3}s) for {controller}/{action}");
        }
    }
}