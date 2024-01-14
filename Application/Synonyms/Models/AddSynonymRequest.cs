using System.ComponentModel.DataAnnotations;

namespace Application.Synonyms.Models
{
    public class AddSynonymRequest
    {
        public int? ParentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
