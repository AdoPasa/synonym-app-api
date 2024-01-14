using Application.Common.Models;

namespace Application.Synonyms.Models
{
    public class SearchSynonymsRequest : PaginatedRequest
    {
        public string? Name { get; set; }
    }
}
