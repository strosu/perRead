using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PerRead.Backend.Filters
{
    public class LogAsyncResourceFilter : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(
            ResourceExecutingContext context,
            ResourceExecutionDelegate next)
        {
            Console.WriteLine("Executing async!");
            ResourceExecutedContext executedContext = await next();
            Console.WriteLine("Executed async!");
        }
    }
}

