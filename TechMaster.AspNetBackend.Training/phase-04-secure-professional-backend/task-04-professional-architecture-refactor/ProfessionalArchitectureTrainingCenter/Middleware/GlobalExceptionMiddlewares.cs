using Microsoft.AspNetCore.Http;
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
    public class GlobalExceptionMiddlewares
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionMiddlewares> logger;

        public GlobalExceptionMiddlewares(RequestDelegate next, ILogger<GlobalExceptionMiddlewares> logger)
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occurred");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null,
                    Errors = new List<string>() { ex.Message }
                };

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
