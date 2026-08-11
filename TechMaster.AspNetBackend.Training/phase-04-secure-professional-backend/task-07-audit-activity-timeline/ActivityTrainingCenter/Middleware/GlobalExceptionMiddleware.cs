using TrainingCenter.Api.Common;
using ValidationTrainingCenter.Common.Exceptions;

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
            catch (NotFoundException ex)
            {
                logger.LogWarning("Resource not found: {Message}", ex.Message);

                await WriteErrorResponse(context, StatusCodes.Status404NotFound, ex.Message, new List<string> { ex.Message });
                
            }
            catch (BusinessRuleException ex)
            {
                logger.LogWarning("Buissness rule voilation {Message}", ex.Message);

                await WriteErrorResponse(context, StatusCodes.Status400BadRequest, ex.Message, new List<string> { ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogWarning("Unauthorized request : {Message}", ex.Message);

                await WriteErrorResponse(context, StatusCodes.Status401Unauthorized, "Authentication required.", new List<string> 
                { "Authentication required." });
            }
            catch (ForbiddenException ex)
            {
                logger.LogWarning("Forbidden request: {Message}",ex.Message);

                await WriteErrorResponse(context,StatusCodes.Status403Forbidden,"Access denied.",
                    new List<string>
                    {
                        "Access denied."
                    });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred TraceId : {TraceId}", context.TraceIdentifier);
                await WriteErrorResponse(context,StatusCodes.Status500InternalServerError,"An unexpected error occurred.",
                    new List<string>
                    {
                        "An internal server error occurred. Please try again later."
                    });

                
            }
        }
        private static async Task WriteErrorResponse(HttpContext context,int statusCode, string message,List<string> errors)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<string>.FailureResponse(message, errors);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
