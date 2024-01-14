namespace Application.Common.Models
{
    public class Response<T>
    {
        public Response(T data)
        {
            Succeeded = true;
            Data = data;
        }

        public Response(List<string> errors)
        {
            Succeeded = false;
            Errors = errors;
        }

        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; } = default!;
    }
}
