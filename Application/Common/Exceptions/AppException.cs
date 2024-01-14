using System.Net;

namespace Application.Common.Exceptions
{
    public class AppException: Exception
    {
        public AppException(string message, HttpStatusCode statusCode): base(message)
        {
            StatusCode = statusCode;
        }

        public AppException(string message, HttpStatusCode statusCode, List<string> errors) : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
