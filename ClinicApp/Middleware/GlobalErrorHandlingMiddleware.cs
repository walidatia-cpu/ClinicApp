using ClinicApp.Core.Constant;
using ClinicApp.Core.DTO;
using System.Net;
using System.Text.Json;

namespace ClinicApp.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionResult = JsonSerializer.Serialize(new CommonResponse { Message = "ServerError", RequestStatus=RequestStatus.ServerError });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
