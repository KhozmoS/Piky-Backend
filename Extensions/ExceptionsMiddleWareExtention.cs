using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using PikyServer.MiddleWares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikyServer.Extensions
{
    public static class ExceptionsMiddleWareExtention
    {
        public static void ConfigureExceptionsHandler( this IApplicationBuilder app )
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
