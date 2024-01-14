namespace Application.Synonyms.Models
{
    public class SynonymResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<SynonymResponse> RelatedSynonyms { get; set; } = new List<SynonymResponse>();
    }
}
