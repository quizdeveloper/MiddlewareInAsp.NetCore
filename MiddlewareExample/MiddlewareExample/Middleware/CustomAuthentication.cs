using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MiddlewareExample.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomAuthentication
    {
        private readonly RequestDelegate _next;

        public CustomAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // START request http code
            var userName = httpContext.Request.Query["username"].ToString();

            //if (httpContext.Response.ContentType.Contains("text/html")) // Only html page
            //{
                if (userName == null) httpContext.Response.Redirect("/home/login");

                if (httpContext.Request.Path != "/home/login")
                {
                    if (userName != "admin" && userName != "administrator")
                    {
                        httpContext.Response.Redirect("/home/login");
                    }
                }
           // }
            // END request http code

            await _next(httpContext);

            // START response http code
              
            // END response http code

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomAuthenticationExtensions
    {
        public static IApplicationBuilder UseCustomAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthentication>();
        }
    }
}
