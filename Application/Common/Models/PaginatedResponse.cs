namespace Application.Common.Models
{
    public class PaginatedResponse<T>: Response<T>
    {
        public PaginatedResponse(T data, int skip, int take, int numberOfResults = 0): base(data)
        {
            Skip = skip;
            Take = take;
            NumberOfResults = numberOfResults;
        }

        public PaginatedResponse(List<string> errors) : base(errors)
        {
            Skip = 0;
            Take = 0;
            NumberOfResults = 0;
        }          
       
        public int NumberOfResults { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
