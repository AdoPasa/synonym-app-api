namespace Application.Common.Models
{
    public class PaginatedRequest
    {
        private const int MaxResults = 50;
        private const int DefaultResults = 10;

        public PaginatedRequest()
        {
        }

        public int Skip { get; set; }
        public int Take { get; set; } = DefaultResults;
        public bool IgnoreNumberOfResults { get; set; } = false;

        public int NormalizedSkip { 
            get {
                if (Skip < 0)
                    return 0;

                return Skip;
            }
        }

        public int NormalizedTake
        {
            get
            {
                if (Take <= 0 || Take > MaxResults)
                    return DefaultResults;

                return Take;
            }
        }
    }
}
