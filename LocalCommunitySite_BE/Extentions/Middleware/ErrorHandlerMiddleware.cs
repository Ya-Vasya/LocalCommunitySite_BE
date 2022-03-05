using LocalCommunitySite.API.Extentions.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace LocalCommunitySite.API.Extentions.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case CustomException e:
                        await HandleExceptionAsync(context, e);
                        break;
                    default:
                        var result = JsonSerializer.Serialize(new 
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Message = "Internal Server Error",
                            StackTrace = error.StackTrace
                        });
                        await response.WriteAsync(result);
                        break;
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, CustomException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.StatusCode;

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
