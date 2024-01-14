using Application.Synonyms.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class SynonymMappers
    {
        public static SynonymResponse ToResponse(this Synonym model) {
            return new SynonymResponse
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };
        }
    }
}
