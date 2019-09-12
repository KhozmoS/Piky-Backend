using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using PikyServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PikyServer.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        // private readonly ILogger _logger;

        public ExceptionMiddleware( RequestDelegate next )
        {
            // _logger = new ILogger();
            _next = next;
        }

        public async Task InvokeAsync( HttpContext httpContext ) 
        {
            try
            {
                await _next(httpContext);
            }
            catch( Exception ex )
            {
                // _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync( httpContext, ex );
            }
        }

        private Task HandleExceptionAsync( HttpContext context , Exception ex )
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = $"Internal Server Error from the custom middleware: {ex.Message}"                
            }.ToString());
        }
    }
}
