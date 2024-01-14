using Application.Synonyms.Models;
using Application.Common.Exceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common.Helpers;
using Application.Common.Models;
using Application.Mappers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Application.Interfaces;

namespace Application.Synonyms
{
    public class SynonymService
    {
        private readonly IAppDbContext _dbContext;
        private readonly ILogger<SynonymService> _logger;
        public SynonymService(
            IAppDbContext dbContext,
            ILogger<SynonymService> logger
            )
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<SynonymResponse?> GetByName(string name)
        {
            var normalizedName = name.NormalizeValue();

            var dbEntry = await _dbContext.Synonyms.FirstOrDefaultAsync(s => s.NormalizedName == normalizedName);

            if (dbEntry == null)
                return null;

            var parentId = dbEntry.ParentId == null ? dbEntry.Id : dbEntry.ParentId;

            var response = dbEntry.ToResponse();

            var relatedSynonyms = await _dbContext.Synonyms
                .Where(s => s.Id != dbEntry.Id && (s.ParentId == parentId || s.Id == parentId))
                .Select(s => s.ToResponse())
                .ToListAsync();

            response.RelatedSynonyms = relatedSynonyms;

            return response;
        }

        public async Task<PaginatedResponse<List<SynonymResponse>>> Search(SearchSynonymsRequest request)
        {
            var query = _dbContext.Synonyms.AsQueryable();

            var normalizedName = request.Name.NormalizeValue();
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.NormalizedName.StartsWith(normalizedName));
            }


            int numberOfResults = 0;
            if (!request.IgnoreNumberOfResults)
            {
                numberOfResults = await query.CountAsync();
            }

            var result = await query
                .Select(s => s.ToResponse())
                .Skip(request.NormalizedSkip)
                .Take(request.NormalizedTake)
                .ToListAsync();

            return new PaginatedResponse<List<SynonymResponse>>(result, request.NormalizedSkip, request.NormalizedTake, numberOfResults);
        }

        public async Task<bool> DoesExists(string requestedSynonym)
        {
            var normalizerSynonym = requestedSynonym.NormalizeValue();
            return (await _dbContext.Synonyms.FirstOrDefaultAsync(s => s.NormalizedName == normalizerSynonym)) != null;
        }

        public async Task<SynonymResponse> Add(AddSynonymRequest request) 
        {
            if (await DoesExists(request.Name))
            {
                _logger.LogError("Tried to add an existing synonym: {0}", JsonConvert.SerializeObject(request));
                throw new AppException("error.synonym.already-added", System.Net.HttpStatusCode.BadRequest);
            }

            if (request.ParentId.HasValue) 
            {
                var parent = await _dbContext.Synonyms.FindAsync(request.ParentId.Value);

                if (parent == null) {
                    _logger.LogError("Tried to add an synonym with an invalid parent word, data: {0}", JsonConvert.SerializeObject(request));
                    throw new AppException("error.synonym.invalid-parent", System.Net.HttpStatusCode.BadRequest);
                }

                // Ensure to always use the top parent as root
                if (parent.ParentId.HasValue)
                {
                    request.ParentId = parent.ParentId;
                }
                else 
                { 
                    request.ParentId = parent.Id;
                }
            }

            var dbEntry = new Synonym
            {
                Name = request.Name.Trim(),
                NormalizedName = request.Name.NormalizeValue(),
                Description = request.Description,
                ParentId = request.ParentId,
            };

            _dbContext.Synonyms.Add(dbEntry);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Synonym successfully added: {0}", JsonConvert.SerializeObject(dbEntry));

            return dbEntry.ToResponse();
        }    
    }
}
