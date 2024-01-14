using System.Text;

namespace WebApi.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // By default multiple requests reading are not allowed, because of that buffering needs to be enabled
            context.Request.EnableBuffering();

            await LogRequest(context.Request);

            await _next(context);

            LogResponse(context.Response);
        }

        private async Task LogRequest(HttpRequest request)
        {
            _logger.LogInformation($"Request received: {request.Method} {request.Path} {request.Query}");
            _logger.LogInformation($"Request headers: {GetHeadersAsString(request.Headers)}");

            if (request.Method == "POST" || request.Method == "PUT")
            {
                _logger.LogInformation($"Request body: {await GetRequestBody(request)}"); 
            }
        }

        private void LogResponse(HttpResponse response)
        {
            _logger.LogInformation($"Response sent: {response.StatusCode}");
            _logger.LogInformation($"Response headers: {GetHeadersAsString(response.Headers)}");
        }

        private string GetHeadersAsString(IHeaderDictionary headers)
        {
            var stringBuilder = new StringBuilder();
            foreach (var (key, value) in headers)
            {
                stringBuilder.AppendLine($"{key}: {value}");
            }
            return stringBuilder.ToString();
        }

        private async Task<string> GetRequestBody(HttpRequest request) 
        {
            var streamReader = new StreamReader(request.Body);
            var body = await streamReader.ReadToEndAsync();

            // "Rewind" the body to be able to read it again
            request.Body.Position = 0;

            return body;
        }
    }
}
