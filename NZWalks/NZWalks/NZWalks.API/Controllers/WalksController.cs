using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto body) {
            // Convert to Domain Model

            var theWalk = await walksRepository.CreateAsync(mapper.Map<Walk>(body));
            // Convert back to DTO

            return Ok(mapper.Map<WalkDto>(theWalk));
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            return Ok(mapper.Map<List<WalkDto>>(await walksRepository.GetAllAsync()));
        }
    }
}
