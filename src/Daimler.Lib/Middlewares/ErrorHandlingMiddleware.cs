using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Daimler.Lib.Logger;

namespace Daimler.Lib.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDaimlerLogger _daimlerLogger;

        public ErrorHandlingMiddleware(RequestDelegate next, IDaimlerLogger daimlerLogger)
        {
            _next = next;
            _daimlerLogger = daimlerLogger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var msg = AggregateInnerMessages(ex);
                _daimlerLogger.Error(ex, msg);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var json = JsonConvert.SerializeObject(new { ErrorMessage = msg });
                await context.Response.WriteAsync(json);
            }
        }
 
        private static string AggregateInnerMessages(Exception ex)
        {
            var temp = ex;
            while (true)
            {
                if (temp?.InnerException == null) break;
                temp = temp.InnerException;
            }

            return temp == null ? string.Empty : temp.Message;
        }
    }

}