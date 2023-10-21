using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
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
        [ValidateModel]
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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id) {
            return Ok(mapper.Map<WalkDto>(await walksRepository.GetByIdAsync(id)));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updatedWalk)
        {
            var updatedWalkModel = mapper.Map<Walk>(updatedWalk);
            var newUpdatedWalk = await walksRepository.UpdateAsync(id, updatedWalkModel);

            if (newUpdatedWalk == null) return NotFound();
            
            return Ok(mapper.Map<WalkDto>(newUpdatedWalk));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var theWalk = await walksRepository.DeleteAsync(id);

            if (theWalk == null) return NotFound();

            return Ok(mapper.Map<WalkDto>(theWalk));
        }


    }
}
