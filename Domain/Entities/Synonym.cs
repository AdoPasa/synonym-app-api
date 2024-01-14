using Domain.Base;

namespace Domain.Entities
{
    public class Synonym: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int? ParentId { get; set; }
        public Synonym? Parent { get; set; }
    }
}
