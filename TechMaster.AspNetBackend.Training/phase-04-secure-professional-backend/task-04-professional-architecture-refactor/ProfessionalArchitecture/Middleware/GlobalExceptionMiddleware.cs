using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TrainingCenter.Api.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrainingCenter.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionMiddleware> logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BadHttpRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = ApiResponse<string>.FailureResponse(
                    ex.Message,
                    new List<string> { ex.Message });

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                var response = ApiResponse<string>.FailureResponse(
                    ex.Message,
                    new List<string> { ex.Message });

                await context.Response.WriteAsJsonAsync(response);
            }
            
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occurred");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = ApiResponse<string>.FailureResponse(
                    ex.Message,
                    new List<string>
                    {
                        ex.Message
                    });

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
