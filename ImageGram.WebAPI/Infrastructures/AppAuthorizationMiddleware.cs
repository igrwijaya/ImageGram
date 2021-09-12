using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageGram.WebAPI.Infrastructures
{
    public class AppAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AppAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader) && authHeader.Any() &&
                    authHeader[0].StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                if (httpContext.Request.Headers.TryGetValue("X-Account-Id", out var param))
                {
                    var token = param.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        httpContext.Request.Headers.Add("Authorization", token);
                    }
                    else
                    {
                        httpContext.Request.Headers.Add("No-Valid-Token", "Empty Token");
                    }
                }
                else
                {
                    httpContext.Request.Headers.Add("No-Valid-Token", "No-Valid-Token");
                }
            }
            finally
            {
                // Call the next middleware delegate in the pipeline 
                await _next.Invoke(httpContext);
            }
        }
    }
}