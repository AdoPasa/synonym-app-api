using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Synonyms;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException error)
            {
                if (context.Response.HasStarted)
                {
                    return;
                }

                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>(new List<string> { error.Message });

                if (error?.StatusCode != null)
                { 
                    response.StatusCode = (int)error.StatusCode;
                }
                else { 
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                
                var result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unhandled error ocurred: {0}", ex.Message);

                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>(new List<string> { ex.Message });

                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}