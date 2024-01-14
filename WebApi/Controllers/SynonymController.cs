using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Synonyms;
using Application.Synonyms.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class SynonymController : BaseApiController
    {
        private SynonymService _synonymService;
        public SynonymController(SynonymService synonymService)
        {
            _synonymService = synonymService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<List<SynonymResponse>>>> Search([FromQuery]SearchSynonymsRequest request)
        {
            request.IgnoreNumberOfResults = true;

            return Ok(await _synonymService.Search(request));
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Response<SynonymResponse>>> GetByName(string name)
        {
            var result = await _synonymService.GetByName(name);

            if (result == null)
                throw new AppException("error.synonym.not-found", System.Net.HttpStatusCode.NotFound);

            return Ok(new Response<SynonymResponse>(result));
        }

        [HttpPost]
        public async Task<ActionResult<Response<SynonymResponse>>> Add(AddSynonymRequest request)
        {
            var result = await _synonymService.Add(request);

            return Ok(new Response<SynonymResponse>(result));
        }
    }
}
