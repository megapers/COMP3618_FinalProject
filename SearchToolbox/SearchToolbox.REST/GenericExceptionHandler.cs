using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SearchToolbox.REST
{
    /// <summary>
    /// Generic exception handler
    /// </summary>
    public class GenericExceptionHandler
    {
        private readonly RequestDelegate _requestDelegate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestDelegate"></param>
        public GenericExceptionHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            string result = JsonConvert.SerializeObject(new { error = exception.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
